namespace Dinex.Core
{
    public struct LaunchResumeByYearAndMonthResponseDto
    {
        public decimal Received { get; set; }
        public decimal Paid { get; set; }
        public bool HasPending { get; set; }
        public decimal TotalAvailable => Received - Paid;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
