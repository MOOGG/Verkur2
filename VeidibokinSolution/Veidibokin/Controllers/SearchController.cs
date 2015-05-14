using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Veidibokin.Models;
using Veidibokin.Repositories;
using System.IO;
using System.Drawing;
using System.Net;
using System.Web.Helpers;

namespace Veidibokin.Controllers
{
    public class SearchController : Controller
    {
		[Authorize]
		public ActionResult SearchResult(string searchString)
		{
			SearchResultViewModel empty = new SearchResultViewModel();
			empty.mySearchResultList = new List<SearchResult>();
			if (!String.IsNullOrEmpty(searchString))
			{
				var mySearchRepo = new SearchRepository();

				var searchResultList = new List<SearchResult>();

				searchResultList = mySearchRepo.ReturnSearchResult(searchString);

				SearchResultViewModel temp = new SearchResultViewModel();

				temp.mySearchResultList = searchResultList;

				return View(temp);
			}
			else
			{
				return View(empty);
			}
		}     
    }
}