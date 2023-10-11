using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> GetVideos()
        {
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            List<VideoDTO> dtolist = new List<VideoDTO>();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.AddDate = item.AddDate;
                dto.ID = item.ID;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == model.ID);
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                video.OriginalVideoPath = model.OriginalVideoPath;
                video.Title = model.Title;
                video.VideoPath = model.VideoPath;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteVideo(int id)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == id);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            Video video = db.Videos.First(x => x.ID == ID);
            VideoDTO dto = new VideoDTO();
            dto.ID = video.ID;
            dto.Title = video.Title;
            dto.OriginalVideoPath = video.OriginalVideoPath;
            return dto;
        }
    }
}
