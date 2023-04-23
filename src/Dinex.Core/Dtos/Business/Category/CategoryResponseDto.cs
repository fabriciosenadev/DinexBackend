namespace Dinex.Core
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Applicable { get; set; }
        public Boolean IsCustom { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
