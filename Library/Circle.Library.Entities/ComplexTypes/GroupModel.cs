using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{

    public class GroupModel
    {
        public Group Group { get; set; }
        public Group GroupEn { get; set; }
        public List<GroupClaimModel> GroupClaims { get; set; }
    }
}
