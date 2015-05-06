using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Catch
    {
        public int ID { get; set;}
        public int userId { get; set; }
        public int zoneId { get; set; }
        public int photoId { get; set; }
        public int fishTypeId { get; set; }
        public int baitTypeID { get; set; }
        public double length { get; set; }
        public double weight { get; set; }
        public string catchText { get; set; }
        public bool isPublic { get; set; }
    }
}