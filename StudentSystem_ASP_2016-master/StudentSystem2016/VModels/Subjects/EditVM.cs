﻿using DataAcsess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentSystem2016.VModels.Subjects
{
    public class EditVM
    {
        public int ID { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1)]
        [MinLength(1)]
        public string Course { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string Semester { get; set; }

        public IEnumerable<SelectListItem> Specialty { get; set; } 
    }
}