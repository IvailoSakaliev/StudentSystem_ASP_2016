﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcsess.Models
{
    public class SpecialtySubject
        :Parent
    {
        public int SpecialtyID { get; set; }
        public int SubjectID {get; set;}
    }
}
