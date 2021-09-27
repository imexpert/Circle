namespace Circle.Core.Entities.Concrete
{
    public class Language : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}