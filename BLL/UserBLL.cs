using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userdao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userdao.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public void AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.AddDate = DateTime.Now;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.isAdmin = model.isAdmin;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            user.NameSurname = model.Name;
            user.Password = model.Password;
            user.Username = model.Username;

            int ID = userdao.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID);
        }

        public List<UserDTO> GetUsers()
        {
            return userdao.GetUsers();
        }

        public UserDTO GetUserWithID(int ID)
        {
            return userdao.GetUserWithID(ID);
        }

        public string UpdateUser(UserDTO model)
        {
            string oldImagePath = userdao.UpdateUser(model);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.Messages.UpdateSuccess, model.ID);
            return oldImagePath;
        }

        public string DeleteUser(int id)
        {
            string imagepath = userdao.DeleteUser(id);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.User, id);
            return imagepath;
        }
    }
}
