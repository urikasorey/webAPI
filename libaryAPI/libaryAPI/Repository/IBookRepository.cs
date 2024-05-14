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


		/*List<BookDTO> GetAllBooks(string? filterOn = null, string?
		filterQuery = null);
		BookDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
		Books? DeleteBookById(int id);*/



		/*List<BookDTO> GetAllBooks(string? filterOn = null, string?
		filterQuery = null, string? sortBy = null, bool isAscending = true);
			BookDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
		Books? DeleteBookById(int id);*/



		
			List<BookDTO> GetAllBooks(string? filterOn = null, string?
			filterQuery = null, string? sortBy = null,
			bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
			BookDTO GetBookById(int id);
			AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
			AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
			Books? DeleteBookById(int id);
		

	}
}