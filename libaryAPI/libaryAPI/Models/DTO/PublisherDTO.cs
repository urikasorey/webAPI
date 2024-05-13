using System.ComponentModel.DataAnnotations;

namespace libaryAPI.Models.DTO
{
	public class PublisherDTO
	{

		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public  int PublisherID { get; set; }
		public List<Books> Books { get; set; }
		public List<string> AuthorNames { get; set; }
		
		// add model to get Book and Author
		public class PublisherWithBooksAndAuthorsDTO
		{
			public string Name { get; set; }
			public List<BookAuthorDTO> BookAuthors { set; get; }
		}
		public class BookAuthorDTO
		{
			public string BookName { get; set; }
			public List<string> BookAuthors { get; set; }
		}
	}
}

