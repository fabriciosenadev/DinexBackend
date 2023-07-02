namespace Dinex.Core;

public class QueueIn : Entity
{
    public Guid UserId { get; set; }
    public TransactionActivity Type { get; set; }
    public string FileName { get; set; }
}
