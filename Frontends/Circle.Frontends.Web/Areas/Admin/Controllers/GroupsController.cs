using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Infrastructure.Extensions;
using Circle.Frontends.Web.Models;
using Circle.Frontends.Web.Services.Abstract;
using Circle.Library.Entities.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupsController : BaseController
    {
        IGroupService _groupService;
        IOperationClaimService _operationClaimService;

        IGroupClaimService _groupClaimService;
        public GroupsController(IGroupService groupService, IOperationClaimService operationClaimService, IGroupClaimService groupClaimService)
        {
            _groupService = groupService;
            _operationClaimService = operationClaimService;
            _groupClaimService = groupClaimService;
        }

        #region sayfalar
        public async Task<IActionResult> List()
        {
            var groupResponse = await _groupService.GetWithClaimsAsync(Guid.Empty);
            return View(groupResponse);
        }
        #endregion

        #region partial işlemleri
        [HttpPost]
        public async Task<IActionResult> GetGroupFormModal(IFormCollection collection)
        {
            ResponseMessage<List<GroupModel>> result = new ResponseMessage<List<GroupModel>>();
            Guid ID = Guid.Parse(collection["ID"]);
            if (ID != Guid.Empty)
            {
                result = await _groupService.GetWithClaimsAsync(ID);
            }
            else
            {
                GroupModel item = new GroupModel();
                item.Group = new Group { GroupName = "", Id = Guid.Empty, LanguageId = LanguageExtension.TrLanguageId};
                item.GroupEn = new Group { GroupName = "", Id = Guid.Empty, LanguageId = LanguageExtension.UsLanguageId};
                item.GroupClaims = new List<GroupClaimModel>();
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Data = new List<GroupModel>();
                result.Data.Add(item);
                result.Data.Add(new GroupModel { Group = new Group { GroupName = "", Id = Guid.Empty, LanguageId = LanguageExtension.UsLanguageId }, GroupClaims = new List<GroupClaimModel>() });
            }
            var xxx = await _operationClaimService.GetList();
            TempData["OperationClaims"] = xxx.Data;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetGroupSilModal(IFormCollection collection)
        {
            Guid ID = Guid.Parse(collection["ID"]);
            return View(await _groupService.GetWithIdAsync(ID));
        }
        #endregion

        #region post işlemleri
        [HttpPost]
        public async Task<ResponseMessage<Group>> AddGroup(IFormCollection collection)
        {
            GroupModel groupModel = new GroupModel();
            groupModel.Group = new Group { Id = Guid.NewGuid(), LanguageId = LanguageExtension.TrLanguageId, GroupName = collection["GroupNameTr"] };
            groupModel.GroupEn = new Group { Id = groupModel.Group.Id, LanguageId = LanguageExtension.UsLanguageId, GroupName = collection["GroupNameEn"] };

            groupModel.GroupClaims = new List<GroupClaimModel>();
            string roleList = collection["roleList"];
            foreach (var item in roleList.Split(','))
            {
                if (item == "" || item == null)
                    continue;
                GroupClaimModel groupClaim = new GroupClaimModel();
                groupClaim.OperationClaimId = Guid.Parse(item);
                groupModel.GroupClaims.Add(groupClaim);
            }

            var result = await _groupService.AddAsync(groupModel);
            //return View(result);
            return result;
        }

        [HttpPost]
        public async Task<ResponseMessage<Group>>/*Task<IActionResult>*/ UpdateGroup(IFormCollection collection)
        {
            GroupModel groupModel = new GroupModel();
            groupModel.Group = new Group { LanguageId = LanguageExtension.TrLanguageId, GroupName = collection["GroupNameTr"], Id = Guid.Parse(collection["Id"]) };
            groupModel.GroupEn = new Group { LanguageId = LanguageExtension.UsLanguageId, GroupName = collection["GroupNameEn"], Id = groupModel.Group.Id };

            groupModel.GroupClaims = new List<GroupClaimModel>();
            string roleList = collection["roleList"];
            foreach (var item in roleList.Split(','))
            {
                if (item == "" || item == null)
                    continue;
                GroupClaimModel groupClaim = new GroupClaimModel();
                groupClaim.OperationClaimId = Guid.Parse(item);
                groupModel.GroupClaims.Add(groupClaim);
            }
            var result = await _groupService.UpdateAsync(groupModel);
            //return View(result);
            return result;
        }

        [HttpPost]
        public async Task<ResponseMessage<List<Group>>> DeleteGroup(IFormCollection collection)
        {
            string xxx = collection["ID"];
            Guid xxxss = Guid.Parse(collection["ID"]);
            var result = await _groupService.DeleteAsync(Guid.Parse(collection["ID"]));
            //return View(result);
            return result;
        }

        #endregion
    }
}
