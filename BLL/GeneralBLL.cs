using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetVideos();
            return dto;
        }

        public GeneralDTO GetPostDetailPageItemsWithID(int id)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.PostDetail = dao.GetPostDetail(id);
            return dto; 
        }
    }
}
