using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();
        // GET: Admin/Video
        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddVideo(VideoDTO model)
        {

            //< iframe width = "560" height = "315" src = "https://www.youtube.com/embed/1tPVVDEDGtE?si=99DCcfmUW4htEkGH" title = "YouTube video player" frameborder = "0"  allowfullscreen ></ iframe >
            //https://www.youtube.com/watch?v=1tPVVDEDGtE&ab_channel=MilanJovanovi%C4%87    //32chars

            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);    // brise prva 32 karaktera URL-a jer su uvek isti
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"" allowfullscreen></iframe>", mergelink);
               // model.VideoPath = String.Format(@"<iframe width = ""300"" height = ""200"" src = ""{0}"" title = ""YouTube video player"" frameborder = ""0""  allowfullscreen ></iframe>", mergelink);
                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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
            return View(model);
        }
        public ActionResult VideoList()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            dtolist = bll.GetVideos();
            return View(dtolist);
        }
        public ActionResult UpdateVideo(int ID)
        {
            VideoDTO dto = new VideoDTO();
            dto = bll.GetVideoWitID(ID);
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);    // brise prva 32 karaktera URL-a jer su uvek isti
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = String.Format(@"< iframe width = ""300"" height = ""200"" src = ""{0}"" title = ""YouTube video player"" frameborder = ""0""  allowfullscreen ></ iframe >", mergelink);
                if (bll.UpdateVideo(model))
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
            return View(model);
        }
        public JsonResult DeleteVideo(int id)
        {
            bll.DeleteVideo(id);
            return Json("");
        }
    }
}