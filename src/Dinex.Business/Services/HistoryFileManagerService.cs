namespace Dinex.Business;

public class HistoryFileManagerService : BaseService, IHistoryFileManager
{
    private readonly IHistoryFileService _historyFileService;
    private readonly IQueueInService _queueInService;

    public HistoryFileManagerService(IMapper mapper,
        INotificationService notification,
        IHistoryFileService historyFileService,
        IQueueInService queueInService)
        : base(mapper, notification)
    {
        _historyFileService = historyFileService;
        _queueInService = queueInService;
    }

    public async Task<HistoryFileResponseDto> ReceiveHistoryFile(HistoryFileRequestDto request, Guid userId)
    {
        if (request.FileHistory == null || request.FileHistory.Length == 0)
            Notification.RaiseError(HistoryFile.Error.FileNotReceived);

        if (Path.GetExtension(request?.FileHistory?.FileName) != ".xlsx")
            Notification.RaiseError(HistoryFile.Error.FileFormatInvalid);

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

        var response = new HistoryFileResponseDto();
        response.QueueInId = queueIn.Id;
        response.FileName = queueIn.FileName;
        response.Type = queueIn.Type;
        response.CreatedAt = queueIn.CreatedAt;
        return response;
    }
}
