using System;

namespace Circle.Core.Entities.Concrete
{
    public class User : AuditEntity
    {
        public User()
        {
            Status = true;
        }
        public Guid DepartmentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string MobilePhones { get; set; }
        public bool Status { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }
}