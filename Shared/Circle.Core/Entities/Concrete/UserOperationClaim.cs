namespace Circle.Core.Entities.Concrete
{
    public class UserOperationClaim : AuditEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}