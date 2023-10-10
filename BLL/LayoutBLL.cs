using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categorydao = new CategoryDAO();
        SocialMediaDAO socialdao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();
        MetaDAO metadao = new MetaDAO();
        AddressDAO addresdao = new AddressDAO();
        PostDAO postado = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategories();

            List<SocialMediaDTO> socialdtolist = new List<SocialMediaDTO>();
            socialdtolist = socialdao.GetSocialMedias();
            dto.Facebook = socialdtolist.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialdtolist.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialdtolist.First(x => x.Link.Contains("instagram"));
            dto.Youtube = socialdtolist.First(x => x.Link.Contains("youtube"));
            dto.Linkedin = socialdtolist.First(x => x.Link.Contains("linkedin"));

            dto.FavDTO = favdao.GetFav();

            dto.Metalist = metadao.GetMetaData();

            List<AddressDTO> addressdtolist= addresdao.GetAddresses();
            dto.Address = addressdtolist.First();

            dto.HotNews = postado.GetHotNews();

            return dto;
        }
    }
}
