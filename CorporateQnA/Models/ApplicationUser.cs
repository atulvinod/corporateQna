using Microsoft.AspNetCore.Identity;

namespace CorporateQnA.Data
{
	public class ApplicationUser : IdentityUser
	{
		public string Position { get; set; }

		public string Location { get; set; }

		public string Department { get; set; }
	}

}