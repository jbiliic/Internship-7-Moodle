namespace Moodle.Domain.Common.Entities
{
    public class BaseEntity
    {
        public const int MaxNameLen = 75;
        public const int MessageMaxLen = 250;
        public const string RegexMailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public int Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
