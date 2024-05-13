using libaryAPI.Models.DTO;
using libaryAPI.Models;

namespace libaryAPI.Repository
{
	public interface IBookRepository
	{/*
		List<BookDTO> GetAllBooks();
		BookDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO BookDTO);
		Books? DeleteBookById(int id);*/
		
		
			List<BookRepository> GetAllBooks(string? filterOn = null, string?
			filterQuery = null);
			BookDTO GetBookById(int id);
			AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
			AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
			Books? DeleteBookById(int id);
		
	}
}