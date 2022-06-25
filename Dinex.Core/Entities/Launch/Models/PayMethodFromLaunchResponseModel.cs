namespace Dinex.Core
{
    public class PayMethodFromLaunchResponseModel
    {
        public int Id { get; set; }
        public string PayMethod { get; set; }
        public int LaunchId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
