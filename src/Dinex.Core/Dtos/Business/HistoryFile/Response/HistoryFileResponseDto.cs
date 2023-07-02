namespace Dinex.Core;

public class HistoryFileResponseDto
{

    public Guid QueueInId { get; set; }
    public TransactionActivity Type { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedAt { get; set; }

}
