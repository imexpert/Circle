using Circle.Frontends.Web.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Controllers
{
    
    public class HomeController : BaseController
    {
        IHttpContextAccessor _httpContextAccessor;
        IGroupService _groupService;

        public HomeController(IHttpContextAccessor httpContextAccessor, IGroupService groupService)
        {
            _groupService = groupService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetList();
            return View();
        }
    }
}
