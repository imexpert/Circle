namespace Circle.Core.Entities.Concrete
{
    public class UserGroup : AuditEntity
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}