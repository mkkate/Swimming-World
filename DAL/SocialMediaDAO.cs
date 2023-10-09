using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SocialMediaDAO : PostContext
    {
        public int AddSocialMedia(SocialMedia social)
        {
            try
            {
                db.SocialMedias.Add(social);
                db.SaveChanges();
                return social.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            try
            {
                List<SocialMedia> list = db.SocialMedias.Where(x => x.isDeleted == false).ToList();
                List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
                foreach (var item in list)
                {
                    SocialMediaDTO dto = new SocialMediaDTO();
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dto.Link = item.Link;
                    dto.Name = item.Name;
                    dtolist.Add(dto);
                }
                return dtolist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMedia socialmedia = db.SocialMedias.First(x => x.ID == ID);
            SocialMediaDTO dto = new SocialMediaDTO();
            dto.ID = socialmedia.ID;
            dto.ImagePath = socialmedia.ImagePath;
            dto.Link = socialmedia.Link;
            dto.Name = socialmedia.Name;
            return dto;
        }

        public string DeleteSocialMedia(int id)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == id);
                string imagepath = social.ImagePath;
                social.isDeleted = true;
                social.DeletedDate = DateTime.Now;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return imagepath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            try
            {
                SocialMedia social = db.SocialMedias.First(x => x.ID == model.ID);
                string oldImagePath = social.ImagePath;
                social.Name = model.Name;
                social.Link = model.Link;
                if (model.ImagePath != null)
                    social.ImagePath = model.ImagePath;
                social.LastUpdateDate = DateTime.Now;
                social.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
