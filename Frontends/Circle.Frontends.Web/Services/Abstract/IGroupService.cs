using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Library.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Services.Abstract
{
    public interface IGroupService
    {
        Task<ResponseMessage<List<Group>>> GetList();
        Task<ResponseMessage<Group>> AddAsync(Group groupModel);
        Task<ResponseMessage<Group>> UpdateAsync(Group groupModel);
        Task<ResponseMessage<List<Group>>> GetWithIdAsync(Guid groupId);
        Task<ResponseMessage<List<Group>>> DeleteAsync(Guid groupId);
        Task<ResponseMessage<List<GroupModel>>> GetWithClaimsAsync(Guid groupId);
    }
}
