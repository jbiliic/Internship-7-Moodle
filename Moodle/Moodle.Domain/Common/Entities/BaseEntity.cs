namespace Moodle.Domain.Common.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
