using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Veidibokin.Models;

namespace Veidibokin.Repositories
{
    public class SearchRepository
    {
    
        public List<SearchResult> ReturnSearchResult(string searchString)
        {
            var returnList = new List<SearchResult>();
            
            using (var dataContext = new ApplicationDbContext())
            {
                var userSearch = (from a in dataContext.Users
                                  where a.fullName.Contains(searchString)
                                  select new {fullname = a.fullName});

                var zoneSearch = (from z in dataContext.Zones
                                  where z.zoneName.Contains(searchString)
                                  select new {zonename = z.zoneName});

                var groupSearch = (from g in dataContext.Groups
                                  where g.groupName.Contains(searchString)
                                  select new {groupname = g.groupName});

                foreach (var user in userSearch)
                {
                    returnList.Add(new SearchResult()
                    {
                        searchResultText = user.fullname,
                        type = 0
                    });
                }

                foreach (var zone in zoneSearch)
                {
                    returnList.Add(new SearchResult()
                    {
                        searchResultText = zone.zonename,
                        type = 1
                    });
                }

                foreach (var group in groupSearch)
                {
                    returnList.Add(new SearchResult()
                    {
                        searchResultText = group.groupname,
                        type = 2
                    });
                }

            }

            return returnList;
        }
    }
}