using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : BaseController
    {
        AddressBLL bll = new AddressBLL();
        // GET: Admin/Address
        public ActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            AddressDTO dto = new AddressDTO();
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    // brise sadrzaj polja
                    ModelState.Clear();
                    model = new AddressDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public ActionResult AddressList()
        {
            List<AddressDTO> dtolist = new List<AddressDTO>();
            dtolist = bll.GetAddresses();
            return View(dtolist);
        }
        public ActionResult UpdateAddress(int ID)
        {
            // da ne bismo pravili novu metodu GetAddressWithID, koristimo vec postojecu metodu GetAddresses kojom iz
            // baze uzimamo sve postojece adrese. Uglavnom nema mnogo adresa, pa ne smeta da ih cuvamo ovako u listi
            List<AddressDTO> list = new List<AddressDTO>(); 
            list = bll.GetAddresses();
            // iz liste uzimamo zahtevanu adresu
            AddressDTO dto = list.First(x => x.ID == ID);  
            return View(dto);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if(bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public JsonResult DeleteAddress(int id)
        {
            bll.DeleteAddress(id);
            return Json("");
        }
    }
}