using System.ComponentModel.DataAnnotations;

namespace libaryAPI.Models
{
	public class Book_Author
	{
		[Key]
		public int ID { get; set; }
		public int AuthorID { get; set; }	
		public int BooksID { get; set; }
		public List<Authors>Authors { get; set; }
	}
}
