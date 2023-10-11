using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            //post.Category = model.
            post.CategoryID = model.CategoryID;
            post.LanguageName = model.Language;
            post.LastUpdateDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserID;
            post.Notification = model.Notification;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Title = model.Title;
            post.ViewCount = model.ViewCount;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);

            int ID = dao.AddPost(post);
            LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID);

            // obrada postojecih relacija (za PostImages i TagList)
            SavePostImage(model.PostImages, ID);
            AddTag(model.TagText, ID);

            return true;
        }

        private void AddTag(string tagText, int PostID)
        {
            string[] tags;
            tags = tagText.Split(',');
            List<PostTag> taglist = new List<PostTag>();
            foreach (var item in tags)
            {
                PostTag tag = new PostTag();
                tag.AddDate = DateTime.Now;
                tag.LastUpdateDate = DateTime.Now;
                tag.LastUpdateUserID = UserStatic.UserID;
                tag.PostID = PostID;
                tag.TagContent = item;
                taglist.Add(tag);
            }
            foreach (var item in taglist)
            {
                int tagID = dao.AddTag(item);
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID);
            }
        }

        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment();
            comment.PostID = model.PostID;
            comment.AddDate = DateTime.Now;
            comment.CommentContent = model.Message;
            comment.Email = model.Email;
            comment.NameSurname = model.Name;
            comment.LastUpdateDate = DateTime.Now;
            comment.LastUpdateUserID = 1;   // jer u db ne moze da bude null
            dao.AddComment(comment);
            return true;
        }

        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            List<PostImage> imagelist = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage();
                image.AddDate = DateTime.Now;
                image.ImagePath = item.ImagePath;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                image.PostID = PostID;
                imagelist.Add(image);
            }
            foreach (var item in imagelist)
            {
                int imageID = dao.AddImage(item);
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
            }
        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostWithID(ID);
            dto.PostImages = dao.GetPostImagesWithPostID(ID);
            List<PostTag> taglist = dao.GetPostTagsWithPostID(ID);
            string tagvalue = "";
            foreach (var item in taglist)
            {
                tagvalue += item.TagContent;
                tagvalue += ',';
            }
            dto.TagText = tagvalue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.Post, model.ID);

            if (model.PostImages != null)
                SavePostImage(model.PostImages, model.ID);

            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);

            return true;
        }

        public string DeletePostImage(int id)
        {
            string imagepath = dao.DeletePostImage(id);
            return imagepath;
        }

        public List<PostImageDTO> DeletePost(int id)
        {
            List<PostImageDTO> imagelist = dao.DeletePost(id);
            LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, id);
            return imagelist;
        }
    }
}
