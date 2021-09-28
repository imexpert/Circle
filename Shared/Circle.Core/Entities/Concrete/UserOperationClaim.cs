using System;

namespace Circle.Core.Entities.Concrete
{
    public class UserOperationClaim : AuditEntity
    {
        public Guid UserId { get; set; }
        public Guid OperationClaimId { get; set; }
    }
}