using System.ComponentModel.DataAnnotations;

namespace libaryAPI.Models
{
	public class Authors
	{
		[Key]
		public int AuthorID { get; set; }	
		public string FullName { get; set; }
		public List<Book_Author> Author { get; set; }
	
		
	}
}
