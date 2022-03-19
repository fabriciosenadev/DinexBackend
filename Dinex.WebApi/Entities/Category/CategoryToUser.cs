namespace Dinex.WebApi.Entities
{
    public class CategoryToUser
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
