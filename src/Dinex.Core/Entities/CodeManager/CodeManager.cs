namespace Dinex.Core
{
    public partial class CodeManager
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public CodeReason Reason { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
