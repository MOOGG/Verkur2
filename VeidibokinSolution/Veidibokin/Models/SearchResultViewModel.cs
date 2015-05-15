using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace Veidibokin.Models
{
    /// <summary>
    /// ViewModelið sem við notuðum til þess að birta leitarniðurstöður
    /// </summary>
    public class SearchResultViewModel
    {
        public List<SearchResult> mySearchResultList { get; set; }
    }
}