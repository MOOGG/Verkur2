using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace Veidibokin.Models
{
    public class SearchResultViewModel
    {
        public List<SearchResult> mySearchResultList { get; set; }
    }
}