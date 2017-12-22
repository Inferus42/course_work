using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Stock.Models
{
    public class Table
    {
        public int id { get; set; }
        public int id_material { get; set; }
        public DateTime date_open { get; set; }
        public DateTime date_close { get; set; }
        public Boolean close { get; set; }



    }
}