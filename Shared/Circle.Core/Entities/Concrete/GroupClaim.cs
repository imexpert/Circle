namespace Circle.Core.Entities.Concrete
{
    public class GroupClaim : AuditEntity
    {
        public int GroupId { get; set; }
        public int ClaimId { get; set; }
    }
}