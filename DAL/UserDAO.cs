using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            T_User user = db.T_User.FirstOrDefault(
                x => x.Username == model.Username &&
                x.Password == model.Password);
            if (user != null && user.ID != 0)
            {
                dto.ID = user.ID;
                dto.Username = user.Username;
                dto.Name = user.NameSurname;
                dto.isAdmin = user.isAdmin;
                dto.ImagePath = user.ImagePath;
            }
            return dto;
        }

        public int AddUser(T_User user)
        {
            try
            {
                db.T_User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<UserDTO> GetUsers()
        {
            try
            {
                List<T_User> list = db.T_User.Where(x => x.isDeleted == false || x.isDeleted == null).OrderBy(x => x.AddDate).ToList();
                List<UserDTO> dtolist = new List<UserDTO>();
                foreach (var item in list)
                {
                    UserDTO dto = new UserDTO();
                    dto.Email = item.Email;
                    dto.ID = item.ID;
                    dto.ImagePath = item.ImagePath;
                    dto.Name = item.NameSurname;
                    dto.Username = item.Username;
                    dtolist.Add(dto);
                }
                return dtolist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteUser(int id)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == id);
                string imagepath = user.ImagePath;
                user.isDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return imagepath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UpdateUser(UserDTO model)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == model.ID);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.Name;
                user.Username = model.Username;
                if (model.ImagePath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                user.Email = model.Email;
                user.isAdmin = model.isAdmin;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = UserStatic.UserID;
                user.Password = model.Password;
                db.SaveChanges();
                return oldImagePath;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserDTO GetUserWithID(int ID)
        {
            try
            {
                T_User user = db.T_User.First(x => x.ID == ID);
                UserDTO dto = new UserDTO();
                dto.ID = user.ID;
                dto.ImagePath = user.ImagePath;
                dto.isAdmin = user.isAdmin;
                dto.Name = user.NameSurname;
                dto.Password = user.Password;
                dto.Username = user.Username;
                dto.Email = user.Email;
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
