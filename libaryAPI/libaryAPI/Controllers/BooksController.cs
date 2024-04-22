using libaryAPI.Data;
using libaryAPI.Models;
using libaryAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Reflection;

namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly dbcontext _dbcontext;
		public BooksController(dbcontext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		[HttpGet("get-all-books")]
		public IActionResult GetAll()
		{
			//var allBooksDomain=_dbcontext.Books.ToList();
			//return Ok(allBooksDomain);
			var allBooksDomain = _dbcontext.Books;
			//map domain models to DTO
			var allBooksDTO = allBooksDomain.Select(Books => new BookDTO()
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



			}).ToList();
			return Ok(allBooksDTO);
		}
		/*	[HttpGet("{id}")]
			public async Task<IActionResult> GetBooks(int ID)
			{
				var Books = await _dbcontext.Books.FindAsync(ID);
				if (Books == null)
				{
					return NoContent();
				}
				return Ok(Books);
			}
			[HttpPost("{id}")]
			public async Task<IActionResult> AddBook(Books book)
			{
				if (book == null)
				{
					return BadRequest("Book cannot be null.");
				}
				var resul = await _dbcontext.Books.AddAsync(book);
				await _dbcontext.SaveChangesAsync();
				return CreatedAtAction(nameof(GetBooks), new { id = book.ID }, book);
			}


			[HttpPut("{id}")]
			public async Task<IActionResult> UpdateBook(int ID, Books book)
			{
				if (ID != book.ID)
				{
					return BadRequest("Book ID mismatch");
				}
				var bookInDb = await _dbcontext.Books.FindAsync(ID);
				if (bookInDb == null)
				{
					return NotFound();
				}
				_dbcontext.Entry(bookInDb).CurrentValues.SetValues(book);
				await _dbcontext.SaveChangesAsync();
				return NoContent();
			}
			[HttpDelete("{id}")]
			public async Task<IActionResult> DeleteBook(int ID)
			{
				var book = await _dbcontext.Books.FindAsync(ID);
				if (book == null)
				{
					return NotFound();
				}
				_dbcontext.Books.Remove(book);
				await _dbcontext.SaveChangesAsync();
				return NoContent();
			}*/
		[HttpGet]
		[Route("get-book-by-id/{id}")]
		public IActionResult GetBookById([FromRoute] int id)
		{
			//get book domain model from db
			var bookWithDomain = _dbcontext.Books.Where(n => n.ID == id);
			if (bookWithDomain == null)
			{
				return NotFound();
			}
			//map domain model to DTO
			var bookWithIdDTO = bookWithDomain.Select(Books => new BookDTO()
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
			});
			return Ok(bookWithIdDTO);
		}
		//add book to db
		[HttpPost("Addbook")]
		public IActionResult AddBook([FromBody] BookDTO bookDTO)
		{
			//map DTO to domain model
			var bookDomain = new Books()
			{
				Title = bookDTO.Title,
				Description = bookDTO.Description,
				isRead = bookDTO.IsRead,
				DateRead = bookDTO.DateRead,
				Rate = bookDTO.Rate,
				Genre = bookDTO.Genre,
				CoverUrl = bookDTO.CoverUrl,
				Publishersid = bookDTO.Pubishersid
				
			};
			_dbcontext.Books.Add(bookDomain);
			_dbcontext.SaveChanges();
			foreach(var id in bookDTO.Pubishersid)
			{
				var publisher = _dbcontext.Publishers.FirstOrDefault(n => n.ID == id);
				if (publisher != null)
				{
					return NotFound();
				}
				var _book_publisher = new Books()
				{
					Publishersid =Publishers.ID,
					ID = bookDomain.ID
				};
				_dbcontext.Books.Add(_book_publisher);
				_dbcontext.SaveChanges();
			}
			
		}
	/*	public IActionResult AddBook([FromBody] BookDTO bookDTO)
		{
			//map DTO to domain model
			var bookDomain = new Books()
			{
				Title = bookDTO.Title,
				Description = bookDTO.Description,
				isRead = bookDTO.IsRead,
				DateRead = bookDTO.DateRead,
				Rate = bookDTO.Rate,
				Genre = bookDTO.Genre,
				CoverUrl = bookDTO.CoverUrl,
				Publishersid=bookDTO.Pubishersid
				
			};
			_dbcontext.Books.Add(bookDomain);
			_dbcontext.SaveChanges();
		
			foreach ( var id in bookDTO.AuthorNames)
			{
				var author = _dbcontext.Authors.FirstOrDefault(n => n.FullName == id);
				if (author != null)
				{
					return NotFound();
				}
				var _book_author = new Book_Author()
				{
					AuthorID=author.AuthorID,
					BooksID = bookDomain.ID
					
				};
				_dbcontext.Book_Author.Add(_book_author);
				_dbcontext.SaveChanges();
			}

		}*/
		//update book in db
		[HttpPut]
		[Route("update-book/{id}")]
		public IActionResult UpdateBook([FromBody] BookDTO bookDTO, [FromRoute] int id)
		{
			//get book from db
			var bookFromDb = _dbcontext.Books.Include(n => n.Book_Authors).FirstOrDefault(n => n.ID == id);
			if (bookFromDb == null)
			{
				return NotFound();
			}
			//map DTO to domain model
			bookFromDb.Title = bookDTO.Title;
			bookFromDb.Description = bookDTO.Description;
			bookFromDb.isRead = bookDTO.IsRead;
			bookFromDb.DateRead = bookDTO.DateRead;
			bookFromDb.Rate = bookDTO.Rate;
			bookFromDb.Genre = bookDTO.Genre;
			bookFromDb.CoverUrl = bookDTO.CoverUrl;
			bookFromDb.Publishers.Name = bookDTO.PublishersName;
			bookFromDb.Book_Authors = bookDTO.AuthorNames.Select(n => new Book_Author()
			{
				Author = new Authors()
				{
					FullName = n
				}
			}).ToList();
			_dbcontext.SaveChanges();
			return Ok();
		}
		//delete book from db
		[HttpDelete]
		[Route("delete-book/{id}")]
		public IActionResult DeleteBook([FromRoute] int id)
		{
			//get book from db
			var bookFromDb = _dbcontext.Books.FirstOrDefault(n => n.ID == id);
			if (bookFromDb == null)
			{
				return NotFound();
			}
			_dbcontext.Books.Remove(bookFromDb);
			_dbcontext.SaveChanges();
			return Ok();
		}	
	}
}

	

