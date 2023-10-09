﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please fill the Username Area")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please fill the Password Area")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please fill the Email Area")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please fill the Name Area")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool isAdmin { get; set; }
        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}
