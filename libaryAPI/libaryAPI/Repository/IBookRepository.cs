using libaryAPI.Models;
using libaryAPI.Models.DTO;

namespace libaryAPI.Repository
{
	public interface IBookRepository
	{
		List<BookDTO> GetAllBooks();
		BookDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO BookDTO);
		Books? DeleteBookById(int id);

	}
}
