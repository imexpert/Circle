namespace Circle.Core.Entities.Concrete
{
    public class Group : AuditEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}