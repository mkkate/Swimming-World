using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        PostBLL postbll = new PostBLL();
        public bool AddCategory(CategoryDTO model)
        {
            Category category = new Category();
            category.AddDate = DateTime.Now;
            category.CategoryName = model.CategoryName;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;

            int ID = dao.AddCategory(category);
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, ID);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            return CategoryDAO.GetCategoriesForDropdown();
        }

        public List<CategoryDTO> GetCategories()
        {
            return dao.GetCategories();
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            return dao.GetCategoryWithID(ID);
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID);
            return true;
        }

        public List<PostImageDTO> DeleteCategory(int id)
        {
            List<Post> postlist = dao.DeleteCategory(id);
            LogDAO.AddLog(General.ProcessType.CategoryDelete, General.TableName.Category, id);

            // nested loop
            List<PostImageDTO> imagelist = new List<PostImageDTO>();
            foreach (var item in postlist)
            {
                List<PostImageDTO> deletedimagelist = postbll.DeletePost(item.ID);
                LogDAO.AddLog(General.ProcessType.PostDelete, General.TableName.Post, item.ID);
                // cuvamo putanje svih obrisanih slika
                foreach (var item2 in deletedimagelist)
                {
                    imagelist.Add(item2);
                }
            }
            return imagelist;
        }
    }
}
