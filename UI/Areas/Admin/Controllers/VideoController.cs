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
            if (ModelState.IsValid)
            {
                MergeWatchAndEmbed(model);
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

        private void MergeWatchAndEmbed(VideoDTO model)
        {
            string path = model.OriginalVideoPath.Substring(32);    // brise prva 32 karaktera URL-a jer su uvek isti
            int ampersend = path.IndexOf("&");  // nakon & su naziv kanala i slicno, i taj zapis je razlicit kod embed i watch
            string mergelink = "https://www.youtube.com/embed/" + path.Substring(0, ampersend);
            model.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder=""0"" allowfullscreen></iframe>", mergelink);
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
                this.MergeWatchAndEmbed(model);
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