namespace Dinex.Core
{
    public class ChartDataResponseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Amount { get; set; }
        public string Applicable { get; set; }
        public string PayMethod { get; set; }
    }
}
