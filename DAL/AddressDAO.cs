using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address add)
        {
            try
            {
                db.Addresses.Add(add);
                db.SaveChanges();
                return add.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AddressDTO> GetAddresses()
        {
            List<Address> list = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<AddressDTO> dtolist = new List<AddressDTO>();
            foreach (var item in list)
            {
                AddressDTO dto = new AddressDTO();
                dto.AddressContent = item.Address1;
                dto.Email = item.Email;
                dto.Fax = item.Fax;
                dto.ID = item.ID;
                dto.LargeMapPath = item.MapPathLarge;
                dto.SmallMapPath = item.MapPathSmall;
                dto.Phone = item.Phone;
                dto.Phone2 = item.Phone2;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                Address add = db.Addresses.First(x => x.ID == model.ID);
                add.Address1 = model.AddressContent;
                add.Email = model.Email;
                add.Fax = model.Fax;
                add.LastUpdateDate = DateTime.Now;
                add.LastUpdateUserID = UserStatic.UserID;
                add.MapPathLarge = model.LargeMapPath;
                add.MapPathSmall = model.SmallMapPath;
                add.Phone = model.Phone;
                add.Phone2 = model.Phone2;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteAddress(int id)
        {
            try
            {
                Address address = db.Addresses.First(x => x.ID == id);
                address.isDeleted = true;
                address.DeletedDate = DateTime.Now;
                address.LastUpdateDate = DateTime.Now;
                address.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
