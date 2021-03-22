using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINumbering.Models
{
    public class CompaniesData
    {
        public string companyName { get; set; }
        public string compInitial { get; set; }
        public int lastGenNumber { get; set; }
        public int totalCols { get; set; }
        public int codeDiffs { get; set; }

        public int upsPerSheet { get; set; }

        public bool isVertical { get; set; }

        public int spaceBetweenSheets { get; set; }

        public int colCntr { get; set; }
        public bool WithScratched { get; set; }
        public string Sufix_Scratch { get; set; }
        public int Scr_len { get; set; }
        public string SONumber { get; set; }
    }
    public class Sonumber
    {
        public int fromNo { get; set; }
        public int ToNo { get; set; }
    }
    public class Cdetails
    {
        public string Company { get; set; }
        public int LastGen { get; set; }
        public string LastModify { get; set; }
    }
    public class SOdetails
    {
        public string Company { get; set; }
        public int From { get; set; }
        public int Upto { get; set; }
        public string GenDate { get; set; }
    }


}
