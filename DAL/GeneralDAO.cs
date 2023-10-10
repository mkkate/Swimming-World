using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<PostDTO> GetSliderPosts()
        {
            List<PostDTO> postdtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == true && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            PostID = p.ID,
                            Title = p.Title,
                            CategoryName = c.CategoryName,
                            SeoLink = p.SeoLink,
                            ViewCount = p.ViewCount,
                            AddDate = p.AddDate
                        }
                      ).OrderByDescending(x => x.AddDate).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.PostID;
                dto.Title = item.Title;
                dto.CategoryName = item.CategoryName;
                dto.SeoLink = item.SeoLink;
                dto.ViewCount = item.ViewCount;
                dto.AddDate = item.AddDate;

                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.PostID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.PostID && x.isApproved == true).Count();

                postdtolist.Add(dto);
            }
            return postdtolist;
        }

        public List<VideoDTO> GetVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> videos = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Take(3).ToList();
            foreach (var item in videos)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.AddDate = item.AddDate;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.VideoPath = item.VideoPath;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetMostViewedPosts()
        {
            List<PostDTO> postdtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x =>x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            PostID = p.ID,
                            Title = p.Title,
                            CategoryName = c.CategoryName,
                            SeoLink = p.SeoLink,
                            ViewCount = p.ViewCount,
                            AddDate = p.AddDate
                        }
                      ).OrderByDescending(x => x.ViewCount).Take(5).ToList();
            foreach (var item in list)
            {
                PostDTO dto = new PostDTO();
                dto.ID = item.PostID;
                dto.Title = item.Title;
                dto.CategoryName = item.CategoryName;
                dto.SeoLink = item.SeoLink;
                dto.ViewCount = item.ViewCount;
                dto.AddDate = item.AddDate;

                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.PostID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.PostID == item.PostID && x.isApproved == true).Count();

                postdtolist.Add(dto);
            }
            return postdtolist;
        }
    }
}
