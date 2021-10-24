using System;

namespace Circle.Core.Entities.Concrete
{
    public class GroupClaim : AuditEntity
    {
        public Guid GroupId { get; set; }
        public Guid OperationClaimId { get; set; }
    }
}