using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        PostBLL bll = new PostBLL();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(PostDTO model)
        {
            // provera da li je izabrana slika
            if (model.PostImage[0] == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                // omoguceno je dodavanje vise slika, pa na ovaj nacin moramo prvo proveriti 
                // ekstenziju svake izabrane slike
                foreach (var item in model.PostImage)
                {
                    // 1. unos slike u tok podataka (stream)
                    Bitmap image = new Bitmap(item.InputStream);
                    // 2. izdvajanje ekstenzije
                    string ext = Path.GetExtension(item.FileName);
                    // 3. provera da li je odgovarajuca ekstenzija
                    if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                }

                // add operacije (za slike)
                List<PostImageDTO> imagelist = new List<PostImageDTO>();
                foreach (var postedfile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedfile.InputStream);
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string uniqueNumber = Guid.NewGuid().ToString();
                    string filename = uniqueNumber + postedfile.FileName;
                    resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                    // dodajemo u listu zbog veze 1..N
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imagelist.Add(dto);
                }
                model.PostImages = imagelist;

                // add operacije (za u db)
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }
        public ActionResult PostList()
        {
            List<PostDTO> postlist = new List<PostDTO>();
            postlist = bll.GetPosts();
            return View(postlist);
        }
        public ActionResult UpdatePost(int ID)
        {
            // za update prikazujemo i tabelu slika, ali to ne zelimo da prikazemo kod add,
            // pa u PostDTO dodajemo property isUpdate = false
            // time radvajamo add i update poglede
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(PostDTO model)
        {
            // kada azuriramo slike, mi zapravo dodajemo nove, a brisemo stare
            IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoriesForDropdown();

            if (ModelState.IsValid)
            {
                if (model.PostImage[0] != null)
                {
                    // ekstenzija svake izabrane slike
                    foreach (var item in model.PostImage)
                    {
                        // 1. unos slike u tok podataka (stream)
                        Bitmap image = new Bitmap(item.InputStream);
                        // 2. izdvajanje ekstenzije
                        string ext = Path.GetExtension(item.FileName);
                        // 3. provera da li je odgovarajuca ekstenzija
                        if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                    }

                    // add operacije (za slike)
                    List<PostImageDTO> imagelist = new List<PostImageDTO>();
                    foreach (var postedfile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.InputStream);
                        Bitmap resizeimage = new Bitmap(image, 750, 422);
                        string uniqueNumber = Guid.NewGuid().ToString();
                        string filename = uniqueNumber + postedfile.FileName;
                        resizeimage.Save(Server.MapPath("~/Areas/Admin/Content/PostImage/" + filename));
                        // dodajemo u listu zbog veze 1..N
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        imagelist.Add(dto);
                    }
                    model.PostImages = imagelist;
                }
                if (bll.UpdatePost(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            model = bll.GetPostWithID(model.ID);
            model.Categories = selectlist;
            model.isUpdate = true;
            return View(model);
        }

        public JsonResult DeletePostImage(int id)
        {
            string imagepath = bll.DeletePostImage(id);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + imagepath));
            }
            return Json("");
        }
        public JsonResult DeletePost(int id)
        {
            // post moze da ima vise slika, pa njihove putanje pamtimo u listu
            List<PostImageDTO> imagelist = bll.DeletePost(id);
            foreach (var item in imagelist)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/PostImage/" + item.ImagePath));
                }
            }
            return Json("");
        }
    }
}