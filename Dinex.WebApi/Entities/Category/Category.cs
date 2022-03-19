namespace Dinex.WebApi.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Applicable Applicable { get; set; }
        public Boolean IsCustom { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
