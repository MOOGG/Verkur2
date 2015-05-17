using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veidibokin.Models;
using Veidibokin.Repositories;

namespace Veidibokin.Controllers
{
    /// <summary>
    /// Þessi Controller erfir ProfileController 
    /// </summary>
    public class CatchController : ProfileController
    {
        /// <summary>
        /// Þetta er HttpGet aðgerð sem skilar tómu Viewi
        /// </summary>
        
        [Authorize]
        [HttpGet]
        public ActionResult PostCatch()
        {
            var viewModel = new UserStatusViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Þetta er HttpPost aðgerð sem tekur inn upplýsingar um veiddan fisk, og nota svo StatusRepository til að vista hann í grunninn
        /// Í lokin er svo kallað í PostStatus aðgerðina í ProfileController þar sem upplýsingar um UserStatus
        /// </summary>
        [Authorize]
        [HttpPost]
        public ActionResult PostCatch(UserStatusViewModel collection)
        {
            if (ModelState.IsValid)
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
            }
            else
            {
                return View("error");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Hér er listi yfir fisktegundir sóttur
        /// </summary>
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

        /// <summary>
        /// Hér er listi yfir beitur sóttur
        /// </summary>
        public static List<SelectListItem> GetBaitTypeDropDown()
        {
            var dataContext = new ApplicationDbContext();
            var myRepo = new UserRepository<BaitType>(dataContext);

            List<SelectListItem> BaitTypeList = new List<SelectListItem>();

            List<BaitType> TempList = myRepo.GetAll().ToList();

            foreach (var temp in TempList)
            {
                BaitTypeList.Add(new SelectListItem() { Text = temp.name, Value = temp.ID.ToString() });
            }

            return BaitTypeList;
        }

        /// <summary>
        /// Hér er listi yfir veiðisvæði sóttur
        /// </summary>
        public static List<SelectListItem> GetZoneDropDown()
        {
            var dataContext = new ApplicationDbContext();
            var myRepo = new UserRepository<Zone>(dataContext);

            List<SelectListItem> ZoneList = new List<SelectListItem>();

            List<Zone> TempList = myRepo.GetAll().ToList();

            foreach (var temp in TempList)
            {
                ZoneList.Add(new SelectListItem() { Text = temp.zoneName, Value = temp.ID.ToString() });
            }
            
            return ZoneList;
        }

    }
}

