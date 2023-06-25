namespace Dinex.Business;

public interface IHistoryFileManager
{
    Task<HistoryFileResponseDto> ReceiveHistoryFile(HistoryFileRequestDto request, Guid userId);
}
