using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class Catch
    {
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public string userId { get; set; }
        [ForeignKey("Zone")]
        public int zoneId { get; set; }
        [ForeignKey("Photo")]
        public int? photoId { get; set; }
        [ForeignKey("FishType")]
        public int fishTypeId { get; set; }
        [ForeignKey("BaitType")]
        public int baitTypeID { get; set; }
        public double length { get; set; }
        public double weight { get; set; }
        public string catchText { get; set; }
        public bool isPublic { get; set; }

        public virtual ApplicationUser UserID { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual FishType FishType { get; set; }
        public virtual BaitType BaitType { get; set; }
    }
}