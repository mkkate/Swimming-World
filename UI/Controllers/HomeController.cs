using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        LayoutBLL layoutbll = new LayoutBLL();
        GeneralBLL generalbll = new GeneralBLL();
        // GET: Home
        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;

            GeneralDTO generaldto = new GeneralDTO();
            generaldto = generalbll.GetAllPosts();
            return View(generaldto);
        }
    }
}