using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
	public class GroupViewModel
	{
		public List<GroupFeed> myFeedList { get; set; }
		public HttpPostedFileBase statusPicture { get; set; }
		public List<GroupMembersList> myFullNameList { get; set; }
		public Feed myFeed { get; set; }
        public int groupId { get; set; }
        public CreateGroup myGroup { get; set; }
        public GroupRequester thisRequester { get; set; }
        public int groupStatusId { get; set; }
	}

	public class GroupMembersList
	{
		public string fullName { get; set; }
		public string userId { get; set; }
	}

    public class CreateGroup
    {
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public HttpPostedFileBase groupPic { get; set; }
    }

    public class GroupRequester
    {
        public int groupId { get; set; }
        public string userId { get; set; }
    }
}