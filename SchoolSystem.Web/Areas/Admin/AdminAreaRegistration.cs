﻿using System.Web.Mvc;

namespace SchoolSystem.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "TeachersManagement", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}