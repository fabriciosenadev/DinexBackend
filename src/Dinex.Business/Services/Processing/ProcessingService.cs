using Dinex.Core;

namespace Dinex.Business;

public class ProcessingService : IProcessingService
{
    private readonly IQueueInRepository _queueInRepository;
    private readonly IHistoryFileRepository _historyFileRepository;
    private readonly IBrokerageRepository _brokerageRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILaunchRepository _launchRepository;
    private readonly ILaunchInvestingRepository _launchInvestingRepository;

    public ProcessingService(IQueueInRepository queueInRepository,
        IHistoryFileRepository historyFileRepository,
        IBrokerageRepository brokerageRepository,
        IProductRepository productRepository,
        ILaunchRepository launchRepository,
        ILaunchInvestingRepository launchInvestingRepository)
    {
        _queueInRepository = queueInRepository;
        _historyFileRepository = historyFileRepository;
        _brokerageRepository = brokerageRepository;
        _productRepository = productRepository;
        _launchRepository = launchRepository;
        _launchInvestingRepository = launchInvestingRepository;
    }

    public async Task ProcessQueueIn(Guid userId)
    {
        var queueIn = await _queueInRepository.ListQueueInAsync();

        foreach (var item in queueIn)
        {
            switch (item.Type)
            {
                case TransactionActivity.Investing:
                    await ProcessInvesting(item.Id, userId);

                    item.UpdatedAt = DateTime.UtcNow;

                    await _queueInRepository.UpdateAsync(item);
                    break;
                case TransactionActivity.FinancialPlanning:
                    // se o arquivo for do tipo FinancialPlanning -> chama a regra de processamento de controle financeiro enviado o Id da fila
                    await ProcessFinancialPlanning(item.Id);
                    break;
            }
        }

    }

    #region investing
    private async Task ProcessInvesting(Guid queueInId, Guid userId)
    {
        var list = await _historyFileRepository.ListHistoryFilesAsync(queueInId);

        foreach (var item in list)
        {
            if (item.ActivityType == InvestingActivity.Transfer || item.ActivityType == InvestingActivity.SettlementTransfer)
            {
                var brokerage = await InvestingBrokerageAddAsync(item.Institution);

                var product = await InvestingProductAddAsync(item.Product.Split('-'));

                await InvestingLaunchAddAsync(item, brokerage, product, userId);

                await _historyFileRepository.DeleteAsync(item);
            }
        }
    }

    private async Task<InvestingBrokerage> InvestingBrokerageAddAsync(string brokerageName)
    {
        brokerageName = brokerageName.Trim();

        var brokerage = await _brokerageRepository.GetByNameAsync(brokerageName);
        if (brokerage is null)
        {
            brokerage = new InvestingBrokerage
            {
                BrokerageName = brokerageName,
                CreatedAt = DateTime.UtcNow,
            };
            await _brokerageRepository.AddAsync(brokerage);
        }
        return brokerage;
    }

    private async Task<InvestingProduct> InvestingProductAddAsync(string[] productData)
    {
        var productCode = productData[0].Trim();

        var investingProduct = await _productRepository.GetByNameAsync(productCode);
        if (investingProduct is null)
        {
            var countPos = productData.Length;

            var companyName = productData[1].Trim();
            if (countPos > 2)
                companyName = $"{companyName} - {productData[2].Trim()}";

            investingProduct = new InvestingProduct
            {
                ProductCode = productCode,
                CompanyName = companyName,
                CreatedAt = DateTime.UtcNow,
            };
            await _productRepository.AddAsync(investingProduct);
        }

        return investingProduct;
    }

    private async Task InvestingLaunchAddAsync(InvestingHistoryFile investingHistoryFile,
        InvestingBrokerage investingBrokerage,
        InvestingProduct investingProduct,
        Guid userId)
    {
        var launch = new Launch
        {
            Activity = TransactionActivity.Investing,
            Date = GetOperationDate(investingHistoryFile.Date),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        };
        await _launchRepository.AddAsync(launch);

        var investingLaunch = new InvestingLaunch
        {
            LaunchId = launch.Id,
            Applicable = investingHistoryFile.Applicable,
            InvestingActivity = investingHistoryFile.ActivityType,
            ProductId = investingProduct.Id,
            UnitPrice = investingHistoryFile.UnitPrice,
            OperationPrice = investingHistoryFile.OperationValue,
            ProductOperationQuantity = investingHistoryFile.Quantity,
            InvestingBrokerageId = investingBrokerage.Id,
            CreatedAt = DateTime.UtcNow,
        };
        await _launchInvestingRepository.AddAsync(investingLaunch);
    }

    private static DateTime GetOperationDate(DateTime dateFromFile)
    {
        DateTime operationDate = dateFromFile.AddDays(-2);

        switch (operationDate.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                operationDate = dateFromFile.AddDays(-4);
                break;
            case DayOfWeek.Saturday:
                operationDate = dateFromFile.AddDays(-3);
                break;
        }

        return operationDate;
    }
    #endregion

    private async Task ProcessFinancialPlanning(Guid queueInId)
    {
        throw new NotImplementedException();
    }
}
