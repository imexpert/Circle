using System;

namespace Circle.Core.Entities.Concrete
{
    public class UserClaim : LightEntity
    {
        public Guid UserId { get; set; }
        public Guid ClaimId { get; set; }
        public string RecordUsername { get; set; }
        public DateTime RecordDate { get; set; }
        public string UpdateUsername { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Ip { get; set; }
    }
}