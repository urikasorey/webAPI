using System.ComponentModel.DataAnnotations;

namespace libaryAPI.Models
{
	public class Authors
	{
		[Key]
		public string FullName { get; set; }
		
	}
}
