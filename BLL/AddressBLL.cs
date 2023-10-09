﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model)
        {
            Address add = new Address();
            add.AddDate = DateTime.Now;
            add.Address1 = model.AddressContent;
            add.Email = model.Email;
            add.Fax = model.Fax;
            add.LastUpdateDate = DateTime.Now;
            add.LastUpdateUserID = UserStatic.UserID;
            add.MapPathLarge = model.LargeMapPath;
            add.MapPathSmall = model.SmallMapPath;
            add.Phone = model.Phone;
            add.Phone2 = model.Phone2;

            int ID = dao.AddAddress(add);
            LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, ID);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAddresses();
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.UpdateAddress(model);
            LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID);
            return true;
        }

        public void DeleteAddress(int id)
        {
            dao.DeleteAddress(id);
            LogDAO.AddLog(General.ProcessType.AddressDelete, General.TableName.Address, id);
        }
    }
}
