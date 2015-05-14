using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veidibokin.Models;
using Veidibokin.Repositories;

namespace Veidibokin.Controllers
{
    public class CatchController : HomeController
    {
        [Authorize]
        public ActionResult PostCatch(UserStatusViewModel collection)
        {
            int zoneID = collection.myCatch.zoneID;
            int baitID = collection.myCatch.baitTypeID;
            int fishID = collection.myCatch.fishTypeId;
            double? length = collection.myCatch.length;
            double? weight = collection.myCatch.weight;

            var myCatchRepo = new StatusRepository();
            Catch newCatch = myCatchRepo.CatchToDB(zoneID, baitID, fishID, length, weight);

            var catchId = newCatch.ID;
            PostStatus(collection, catchId);

            return RedirectToAction("Index", "Home");
        }

        public static List<SelectListItem> GetFishTypeDropDown()
        {
            var dataContext = new ApplicationDbContext();
            var myRepo = new UserRepository<FishType>(dataContext);

            List<SelectListItem> FishTypeList = new List<SelectListItem>();

            List<FishType> TempList = myRepo.GetAll().ToList();

            foreach (var temp in TempList)
            {
                FishTypeList.Add(new SelectListItem() { Text = temp.name, Value = temp.ID.ToString() });
            }

            return FishTypeList;
        }

    }
}

