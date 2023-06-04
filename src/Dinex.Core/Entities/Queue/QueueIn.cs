namespace Dinex.Core;

public class QueueIn : Entity
{
    public Guid UserId { get; set; }
    public QueueType Type { get; set; }
    public string FileName { get; set; }
}
