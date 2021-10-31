using Circle.Core.Entities.Concrete;
using Circle.Core.Utilities.Results;
using Circle.Frontends.Web.Controllers;
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
                item.Group_ = new Group { GroupName = "", Id = Guid.Empty };
                item.GroupClaims = new List<GroupClaimModel>();
                result.IsSuccess = true;
                result.StatusCode = 200;
                result.Data = new List<GroupModel>();
                result.Data.Add(item);
            }

            //ViewBag.ID = ID;
            TempData["OperationClaims"] = await _operationClaimService.GetList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetGroupSilModal(IFormCollection collection)
        {
            PageItem item = new PageItem();
            Guid ID = Guid.Parse(collection["ID"]);
            if (ID != Guid.Empty)
            {
                var result = await _groupService.GetList();
                item.listGroup = result.Data;
            }
            else
            {
                item.listGroup.Add(new Group { Id = Guid.Empty });
            }
            return View(item);
        }
        #endregion

        #region post işlemleri
        [HttpPost]
        public async Task<IActionResult> AddGroup(IFormCollection collection)
        {
            Group group = new Group();
            group.Id = Guid.NewGuid();
            group.GroupName = collection["GroupName"];

            var result = await _groupService.AddAsync(group);
            if (result.IsSuccess)
            {
                string roleList = collection["roleList"];
                foreach (var item in roleList.Split(','))
                {
                    GroupClaim groupClaim = new GroupClaim();
                    groupClaim.Id = Guid.Parse(item.Split('#')[1].ToString());
                    groupClaim.OperationClaimId = Guid.Parse(item.Split('#')[0].ToString());
                    groupClaim.GroupId = result.Data.Id;
                    if (groupClaim.Id == Guid.Empty)
                        await _groupClaimService.AddAsync(groupClaim);
                    else
                        await _groupClaimService.UpdateAsync(groupClaim);
                }
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGroup(IFormCollection collection)
        {
            Group group = new Group();
            group.Id = Guid.Parse(collection["Id"]);
            group.GroupName = collection["GroupName"];
            var result = await _groupService.UpdateAsync(group);
            if (result.IsSuccess)
            {
                string roleList = collection["roleList"];
                foreach (var item in roleList.Split(','))
                {
                    GroupClaim groupClaim = new GroupClaim();
                    groupClaim.Id = Guid.Parse(item.Split('#')[1].ToString());
                    groupClaim.OperationClaimId = Guid.Parse(item.Split('#')[0].ToString());
                    groupClaim.GroupId = result.Data.Id;
                    if (groupClaim.Id == Guid.Empty)
                        await _groupClaimService.AddAsync(groupClaim);
                    else
                        await _groupClaimService.UpdateAsync(groupClaim);
                }
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(IFormCollection collection)
        {
            var result = await _groupService.DeleteAsync(Guid.Parse(collection["ID"]));
            return View(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> GroupIslem(IFormCollection collection)
        //{
        //    string type = collection["type"];
        //    Group item = new Group();
        //    item.Id = Guid.Parse(collection["ID"]);
        //    if (type == "Kaydet")
        //    {
        //        item.GroupName = collection["txtGroupName"];
        //        if (item.Id != Guid.Empty)
        //        {
        //            var result = await _groupService.AddAsync(item);
        //            return View(result);
        //        }
        //        else
        //        {
        //            var result = await _groupService.UpdateAsync(item);
        //            return View(result);
        //        }
        //    }
        //    else if (type == "Sil")
        //    {
        //        var result = await _groupService.DeleteAsync(item.Id);
        //        return View(result);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        #endregion
    }
}
