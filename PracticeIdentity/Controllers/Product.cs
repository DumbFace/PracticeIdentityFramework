using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeIdentity.Permissions;

namespace PracticeIdentity.Controllers
{
    public class Product : Controller
    {
        [Authorize(PermissionsAuthorize.Product.View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(PermissionsAuthorize.Product.Create)]
        public IActionResult Create()
        {
            return Content("Create");
        }

        [Authorize(PermissionsAuthorize.Product.Edit)]
        public IActionResult Edit()
        {
            return Content("Edit");
        }

        [Authorize(PermissionsAuthorize.Product.Delete)]
        public IActionResult Delete()
        {
            return Content("Delete");
        }
    }
}
