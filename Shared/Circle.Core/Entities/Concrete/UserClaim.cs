namespace Circle.Core.Entities.Concrete
{
    public class UserClaim : AuditEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}