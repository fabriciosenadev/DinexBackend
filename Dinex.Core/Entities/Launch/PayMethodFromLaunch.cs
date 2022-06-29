namespace Dinex.Core
{
    public partial class PayMethodFromLaunch
    {
        public int Id { get; set; }
        public LaunchPayMethod PayMethod { get; set; }
        public int LaunchId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
