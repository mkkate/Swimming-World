﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTO
{
    public class PostDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please fill the Title area")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill the Short Content area")]
        public string ShortContent { get; set; }
        [Required(ErrorMessage = "Please fill the Post Content area")]
        public string PostContent { get; set; }
        [Required(ErrorMessage = "Please select the Category")]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [Display(Name = "Post Image")]
        public List<PostImageDTO> PostImages { get; set; }
        public List<HttpPostedFileBase> PostImage { get; set; }
        public List<TagDTO> TagList { get; set; }
        public string TagText { get; set; }
        public int ViewCount { get; set; }
        public string SeoLink { get; set; }
        public bool Slider { get; set; }
        public bool Area1 { get; set; }
        public bool Area2 { get; set; }
        public bool Area3 { get; set; }
        public string Language { get; set; }
        public bool Notification { get; set; }
        public DateTime AddDate { get; set; }

        // za razdvajanje add i update pogleda (view)
        public bool isUpdate { get; set; } = false;
    }
}