using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            List<Category> list = db.Categories.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            List<CategoryDTO> dtolist = new List<CategoryDTO>();
            foreach (var item in list)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.CategoryName = item.CategoryName;
                dto.ID = item.ID;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            // kreiramo SelectListItem pomocu list conversion
            IEnumerable<SelectListItem> categoryList = db.Categories
                .Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate)  //ovim smo dobili listu kategorija
                .Select(x => new SelectListItem()   //sada tu listu konvertujemo
                {
                    Text = x.CategoryName,
                    Value = SqlFunctions.StringConvert((double)x.ID)
                }).ToList();
            return categoryList;
        }

        public List<Post> DeleteCategory(int id)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == id);
                category.isDeleted = true;
                category.DeletedDate = DateTime.Now;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                List<Post> postlist = db.Posts.Where(x => x.isDeleted == false && x.CategoryID == id).ToList();
                return postlist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == model.ID);
                category.CategoryName = model.CategoryName;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == ID);
                CategoryDTO dto = new CategoryDTO();
                dto.ID = category.ID;
                dto.CategoryName = category.CategoryName;
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
