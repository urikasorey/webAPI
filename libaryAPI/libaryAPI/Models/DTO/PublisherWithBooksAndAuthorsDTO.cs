
namespace libaryAPI.Models.DTO
{
	public class PublisherWithBooksAndAuthorsDTO
	{
		public string Name { get; set; }
		public List<BookAuthorDTO> BookAuthors { set; get; }
	}
}
