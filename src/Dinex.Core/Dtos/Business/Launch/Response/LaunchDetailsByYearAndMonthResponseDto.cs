namespace Dinex.Core
{
    public struct LaunchDetailsByYearAndMonthResponseDto
    {
        public List<LaunchResponseDto> Launches { get; set; }
        public List<ChartDataResponseDto> PieChartData { get; set; }
    }
}
