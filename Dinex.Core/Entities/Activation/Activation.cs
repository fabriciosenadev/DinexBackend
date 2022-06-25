namespace Dinex.Core
{
    public class Activation
    {
        public int Id { get; set; }
        public string ActivationCode { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
