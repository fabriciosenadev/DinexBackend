namespace Dinex.Business;

public class HistoryFileManagerService : BaseService, IHistoryFileManager
{
    private readonly IHistoryFileService _historyFileService;
    private readonly IQueueInService _queueInService;
    private readonly IProcessingService _processingService;

    public HistoryFileManagerService(IMapper mapper,
        INotificationService notification,
        IHistoryFileService historyFileService,
        IQueueInService queueInService,
        IProcessingService processingService)
        : base(mapper, notification)
    {
        _historyFileService = historyFileService;
        _queueInService = queueInService;
        _processingService = processingService;
    }

    public async Task<HistoryFileResponseDto> ReceiveHistoryFile(HistoryFileRequestDto request, Guid userId)
    {
        if (request.FileHistory == null || request.FileHistory.Length == 0)
            Notification.RaiseError(InvestingHistoryFile.Error.FileNotReceived);

        if (Path.GetExtension(request?.FileHistory?.FileName) != ".xlsx")
            Notification.RaiseError(InvestingHistoryFile.Error.FileFormatInvalid);

        if(Notification.HasNotification())
            return default;

        #region save at the queueIn
        var queueIn = new QueueIn
        {
            UserId = userId,
            FileName = request.FileHistory.FileName,
            Type = request.QueueType,
        };
        await _queueInService.CreateAsync(queueIn);
        #endregion

        // save file at the database
        await _historyFileService.CreateAsync(request.FileHistory, queueIn.Id);

        // TODO: process file in an async method without awaiter
        _processingService.ProcessQueueIn(userId);

        var response = new HistoryFileResponseDto();
        response.QueueInId = queueIn.Id;
        response.FileName = queueIn.FileName;
        response.Type = queueIn.Type;
        response.CreatedAt = queueIn.CreatedAt;
        return response;
    }
}
