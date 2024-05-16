using libaryAPI.Data;
using libaryAPI.Models;
using libaryAPI.Models.DTO;
using libaryAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Reflection;
using System.Text.Json;

namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class BooksController : ControllerBase
	{
		private readonly dbcontext _dbContext;
		private readonly IBookRepository _bookRepository;
		//private readonly ILogger<BooksController> _logger;
		public BooksController(dbcontext dbContext, IBookRepository bookRepository
		/*ILogger<BooksController> logger*/)
		{
			_dbContext = dbContext;
			_bookRepository = bookRepository;
			/*_logger = logger;*/
		}
		//get all books
		// GET: /api/Books/get-all-books?filterOn=Name&filterQuery=Track
		[HttpGet("get-all-books")]
		[Authorize(Roles = "Read")]
		public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string?
		filterQuery,
		[FromQuery] string? sortBy, [FromQuery] bool isAscending,
		[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
		{
			/*_logger.LogInformation("GetAll Book Action method was invoked");
			_logger.LogWarning("This is a warning log");
			_logger.LogError("This is a error log");*/
			// su dung reposity pattern
			var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy,
			isAscending, pageNumber, pageSize);
			//debug
			/*	_logger.LogInformation($"Finished GetAllBook request with data{ JsonSerializer.Serialize(allBooks)}");*/
			return Ok(allBooks);
		}

		
		[HttpGet]
		[Route("get-book-by-id/{id}")]
		public IActionResult GetBookById([FromRoute] int id)
		{
			var bookWithIdDTO = _bookRepository.GetBookById(id);
			return Ok(bookWithIdDTO);
		}
		[HttpPost("Add Book")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
			return Ok(bookAdd);
		}
		[HttpPut("update-book-by-id/{id}")]
		public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
		{
			var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
			return Ok(updateBook);
		}
		[HttpDelete("delete-book-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			var deleteBook = _bookRepository.DeleteBookById(id);
			return Ok(deleteBook);
		}
	}


}
	



	

