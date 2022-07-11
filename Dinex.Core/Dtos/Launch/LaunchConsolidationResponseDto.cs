namespace Dinex.Core
{
    public class LaunchConsolidationResponseDto
    {
        public decimal Received { get; set; }
        public decimal Paid { get; set; }
        public decimal TotalAvailable => Received - Paid;
        
    }
}
