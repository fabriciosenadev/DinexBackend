namespace Dinex.Core
{
    public class LaunchDetailsByYearAndMonthResponseDto
    {
        public List<LaunchResponseDto> Launches { get; set; }
        public List<ChartDataResponseDto> PieChartData { get; set; }
    }
}
