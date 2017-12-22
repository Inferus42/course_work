using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Stock.Models
{
    public class Record
    {
        public int id { get; set; }
        public int id_table { get; set; }
        public string name_material { get; set; }
        public DateTime date_time { get; set; }
        public Boolean supply_or_delivery { get; set; }//
        public int qantity { get; set; }//
        public string provider { get; set; }
        public string worker { get; set; }
        public Boolean check { get; set; }


    }
}