using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Models.Attributes
{
    public class LoginControl : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ako se nije logovao korisnik, preusmeri ga da to uradi
            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            if (UserStatic.UserID == 0)    //hoce nekad da zabaguje za user-a gornji uslov, pa koristimo ovaj
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
        }
    }
}