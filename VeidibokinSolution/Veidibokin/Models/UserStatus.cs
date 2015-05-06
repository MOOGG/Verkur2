using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
    public class UserStatus
    {
        public int ID { get; set; }
        public System.DateTime dateInserted { get; set; }
        public string statusText { get; set; }
        public int userId { get; set; }
        public int photoId { get; set; }
        public bool isPublic { get; set; }
    }
}