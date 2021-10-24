using Circle.Frontends.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OperationClaimsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
