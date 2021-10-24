using Circle.Core.Entities.Concrete;
using Circle.Frontends.Web.Controllers;
using Circle.Frontends.Web.Models;
using Circle.Frontends.Web.Services.Abstract;
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
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        #region sayfalar
        public async Task<IActionResult> List()
        {
            var groupResponse = await _groupService.GetList();
            return View(groupResponse);
        }
        #endregion

        #region partial işlemleri
        [HttpPost]
        public async Task<IActionResult> GetGroupFormModal(IFormCollection collection)
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
        public async Task<IActionResult> AddGroup(Group group)
        {
            return View(await _groupService.AddAsync(group));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            return View(await _groupService.UpdateAsync(group));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(IFormCollection collection)
        {
            return View(await _groupService.DeleteAsync(Guid.Parse(collection["ID"])));
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
