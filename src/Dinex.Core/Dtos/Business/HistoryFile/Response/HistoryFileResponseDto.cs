namespace Dinex.Core;

public class HistoryFileResponseDto
{

    public Guid QueueInId { get; set; }
    public QueueType Type { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedAt { get; set; }

}
