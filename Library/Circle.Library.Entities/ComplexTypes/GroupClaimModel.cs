using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class GroupClaimModel 
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid OperationClaimId { get; set; }
        public string Description { get; set; }
    }
}
