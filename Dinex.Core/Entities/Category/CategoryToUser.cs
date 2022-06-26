namespace Dinex.Core
{
    public class CategoryToUser
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public Applicable Applicable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
