using libaryAPI.Data;
using libaryAPI.Models;
using libaryAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
namespace libaryAPI.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly dbcontext dbcontext1;
		public BookRepository(dbcontext dbcontext)
		{
			dbcontext1 = dbcontext;
		}
		public List<BookDTO> GetAllBooks()
		{
			var allBooksDomain = dbcontext1.Books;
			var allBooksDTO = allBooksDomain.Select(Books => new BookDTO()
			{
				Id = Books.ID,
				Title = Books.Title,
				Description = Books.Description,
				IsRead = Books.isRead,
				DateRead = Books.DateRead,
				Rate = Books.Rate,
				Genre = Books.Genre,
				CoverUrl = Books.CoverUrl,
				DateAdded = Books.DateAdded,
				PublishersName = Books.Publishers.Name,
				AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
			}).ToList();
			return allBooksDTO;
		}
		/*	public List<BookDTO> GetAllBooks(string? filterOn = null, string?
	filterQuery = null, string? sortBy = null, bool isAscending = true)
			{
				var allBooks = dbcontext1.Books.Select(Books => new BookDTO()
				{
					Id = Books.ID,
					Title = Books.Title,
					Description = Books.Description,
					IsRead = Books.isRead,
					DateRead = Books.isRead ? Books.DateRead.Value : null,
					Rate = Books.isRead ? Books.Rate.Value : null,
					Genre = Books.Genre,
					CoverUrl = Books.CoverUrl,
					PublishersName = Books.Publishers.Name,
					AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
				}).AsQueryable();
				//filtering
				if (string.IsNullOrWhiteSpace(filterOn) == false &&
				string.IsNullOrWhiteSpace(filterQuery) == false)
				{
					if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
					{
						allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
					}
				}
				//sorting
				if (string.IsNullOrWhiteSpace(sortBy) == false)
				{
					if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
					{
						allBooks = isAscending ? allBooks.OrderBy(x => x.Title) :
						allBooks.OrderByDescending(x => x.Title);
					}
				}
				return allBooks.ToList();
			}*/

		public List<BookDTO> GetAllBooks(string? filterOn = null, string?
filterQuery = null,
string? sortBy = null, bool isAscending = true, int pageNumber = 1, int
pageSize = 1000)
		{
			var allBooks = dbcontext1.Books.Select(Books => new
			BookDTO()
			{
				Id = Books.ID,
				Title = Books.Title,
				Description = Books.Description,
				IsRead = Books.isRead,
				DateRead = Books.isRead ? Books.DateRead.Value : null,
				Rate = Books.isRead ? Books.Rate.Value : null,
				Genre = Books.Genre,
				CoverUrl = Books.CoverUrl,
				PublishersName = Books.Publishers.Name,
				AuthorNames = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
			}).AsQueryable();
			//filtering
			if (string.IsNullOrWhiteSpace(filterOn) == false &&
			string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
				}
			}
			//sorting
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = isAscending ? allBooks.OrderBy(x => x.Title) :
					allBooks.OrderByDescending(x => x.Title);
				}
			}
			//pagination
			var skipResults = (pageNumber - 1) * pageSize;
			return allBooks.Skip(skipResults).Take(pageSize).ToList();
		}

		public BookDTO GetBookById(int id)
		{
			var book = dbcontext1.Books.FirstOrDefault(n => n.ID == id);
			if (book == null)
			{
				return null;
			}
			var bookDTO = new BookDTO()
			{
				Id = book.ID,
				Title = book.Title,
				Description = book.Description,
				IsRead = book.isRead,
				DateRead = book.DateRead,
				Rate = book.Rate,
				Genre = book.Genre,
				CoverUrl = book.CoverUrl,
				DateAdded = book.DateAdded,
				PublishersName = book.Publishers.Name,
				AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
			};
			return bookDTO;
		}
		public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
		{
			//map DTO to domain model
			var book = new Books()
			{
				Title = addBookRequestDTO.Title,
				Description = addBookRequestDTO.Description,
				isRead = addBookRequestDTO.isRead,
				DateRead = addBookRequestDTO.DateRead,
				Rate = addBookRequestDTO.Rate,
				Genre = addBookRequestDTO.Genre,
				CoverUrl = addBookRequestDTO.CoverUrl,
				DateAdded = addBookRequestDTO.DateAdded,
				PublishersID = addBookRequestDTO.PublisherID
			};
			//use Domain model to add book
			dbcontext1.Books.Add(book);
			dbcontext1.SaveChanges();
			foreach (var authorId in addBookRequestDTO.AuthorIDs)
			{
				var bookAuthor = new Book_Author()
				{
					ID = book.ID,
					AuthorID = authorId
				};
				dbcontext1.Book_Author.Add(bookAuthor);
				dbcontext1.SaveChanges();
			}
			return addBookRequestDTO;
		}
		public AddBookRequestDTO UpdateBookById(int id, AddBookRequestDTO bookDTO)
		{
			var BookDomain = dbcontext1.Books.FirstOrDefault(n => n.ID == id);
			if (BookDomain != null)
			{
				BookDomain.Title = bookDTO.Title;
				BookDomain.Description = bookDTO.Description;
				BookDomain.isRead = bookDTO.isRead;
				BookDomain.DateRead = bookDTO.DateRead;
				BookDomain.Rate = bookDTO.Rate;
				BookDomain.Genre = bookDTO.Genre;
				BookDomain.CoverUrl = bookDTO.CoverUrl;
				BookDomain.DateAdded = bookDTO.DateAdded;
				BookDomain.PublishersID = bookDTO.PublisherID;
				dbcontext1.SaveChanges();
			}
			var AuthorDomain = dbcontext1.Book_Author.Where(n => n.BooksID == id).ToList();
			if (AuthorDomain != null)
			{
				dbcontext1.Book_Author.RemoveRange(AuthorDomain);
				dbcontext1.SaveChanges();
			}
			foreach (var authorId in bookDTO.AuthorIDs)
			{
				var bookAuthor = new Book_Author()
				{
					BooksID = BookDomain.ID,
					AuthorID = authorId
				};
				dbcontext1.Book_Author.Add(bookAuthor);
				dbcontext1.SaveChanges();
			}
			return bookDTO;
		}
		public Books? DeleteBookById(int id)
		{
			var bookDomain = dbcontext1.Books.Where(n => n.ID == id).FirstOrDefault();
			if (bookDomain != null)
			{
				dbcontext1.Books.Remove(bookDomain);
				dbcontext1.SaveChanges();
			}
			return bookDomain;
		}
	}
}