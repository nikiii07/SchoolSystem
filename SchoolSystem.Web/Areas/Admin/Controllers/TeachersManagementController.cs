using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSystem.Web.Areas.Admin.Controllers
{
    public class TeachersManagementController : Controller
    {
        // GET: Admin/TeacherManagement
        public ActionResult Index()
        {
            return View();
        }
    }
}