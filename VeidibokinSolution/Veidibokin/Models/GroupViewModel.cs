using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Veidibokin.Models
{
	public class GroupViewModel
	{
		// tekið ur profile view model
		public List<GroupFeed> myFeedList { get; set; }
		public HttpPostedFileBase statusPicture { get; set; }
		public List<GroupMembersList> myFullNameList { get; set; } 
	}

	public class GroupMembersList
	{
		public string fullName { get; set; }
		public string userId { get; set; }
	}
}