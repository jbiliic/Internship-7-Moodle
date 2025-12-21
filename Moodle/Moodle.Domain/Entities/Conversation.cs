using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities
{
    public class Conversation : BaseEntity
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }

        public User User1 { get; set; } = null!;
        public User User2 { get; set; } = null!;
    }
}
