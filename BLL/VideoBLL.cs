using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VideoBLL
    {
        VideoDAO dao = new VideoDAO();
        public bool AddVideo(VideoDTO model)
        {
            Video video = new Video();
            video.AddDate = DateTime.Now;
            video.AddUserID = UserStatic.UserID;
            video.LastUpdateDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserID;
            video.OriginalVideoPath = model.OriginalVideoPath;
            video.Title = model.Title;
            video.VideoPath = model.VideoPath;

            int ID = dao.AddVideo(video);
            LogDAO.AddLog(General.ProcessType.VideoAdd, General.TableName.Video, ID);
            return true;

        }

        public List<VideoDTO> GetVideos()
        {
            return dao.GetVideos();
        }

        public VideoDTO GetVideoWitID(int ID)
        {
            return dao.GetVideoWithID(ID);
        }

        public bool UpdateVideo(VideoDTO model)
        {
            dao.UpdateVideo(model);
            LogDAO.AddLog(General.ProcessType.VideoUpdate, General.TableName.Video, model.ID);
            return true;
        }

        public void DeleteVideo(int id)
        {
            dao.DeleteVideo(id);
            LogDAO.AddLog(General.ProcessType.VideoDelete, General.TableName.Video, id);
        }
    }
}
