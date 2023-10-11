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

        public PostDTO GetPostDetail(int id)
        {
            Post post = db.Posts.First(x => x.ID == id);
            post.ViewCount++;
            db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.Language = post.LanguageName;
            dto.SeoLink = post.SeoLink;

            dto.CategoryID = post.CategoryID;
            Category category = db.Categories.First(x => x.ID == dto.CategoryID);
            dto.CategoryName = category.CategoryName;

            List<PostImage> images = db.PostImages.Where(x => x.isDeleted == false && x.PostID == id).ToList();
            List<PostImageDTO> imagedtolist = new List<PostImageDTO>();
            foreach (var item in images)
            {
                PostImageDTO img = new PostImageDTO();
                img.ID = item.ID;
                img.ImagePath = item.ImagePath;
                imagedtolist.Add(img);
            }
            dto.PostImages = imagedtolist;

            dto.CommentCount = db.Comments.Where(x => x.isDeleted == false && x.isApproved == true && x.PostID == id).Count();
            List<Comment> comments = db.Comments.Where(x => x.isDeleted == false && x.isApproved == true && x.PostID == id).ToList();
            List<CommentDTO> commentdtolist = new List<CommentDTO>();
            foreach (var item in comments)
            {
                CommentDTO commentdto = new CommentDTO();
                commentdto.AddDate = item.AddDate;
                commentdto.CommentContent = item.CommentContent;
                commentdto.Email = item.Email;
                commentdto.ID = item.ID;
                commentdto.Name = item.NameSurname;
                commentdtolist.Add(commentdto);
            }
            dto.CommentList = commentdtolist;

            List<PostTag> tags = db.PostTags.Where(x => x.isDeleted == false && x.PostID == id).ToList();
            List<TagDTO> tagdtolist = new List<TagDTO>();
            foreach (var item in tags)
            {
                TagDTO tagdto = new TagDTO();
                tagdto.ID = item.ID;
                tagdto.TagContent = item.TagContent;
                tagdtolist.Add(tagdto);
            }
            dto.TagList = tagdtolist;

            return dto;
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
            var list = (from p in db.Posts.Where(x => x.isDeleted == false)
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
