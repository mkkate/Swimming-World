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
        PostBLL postbll = new PostBLL();
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
        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            return View();
        }
        public ActionResult PostDetail(int id)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;

            GeneralDTO generaldto = new GeneralDTO();
            generaldto = generalbll.GetPostDetailPageItemsWithID(id);
            return View(generaldto);
        }
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
            if (model.Name != null && model.Email != null && model.Message != null)
            {
                if (postbll.AddComment(model))
                {
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }

            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutbll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;

            GeneralDTO generaldto = new GeneralDTO();
            model = generalbll.GetPostDetailPageItemsWithID(model.PostID);

            return View(model);
        }
    }
}