﻿using libaryAPI.Data;
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
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBooks(int ID)
		{
			var Books = await _dbcontext.Books.FindAsync(ID);
			if (Books == null)
			{
				return NoContent();
			}
			return Ok(Books);
		}
		[HttpPost]
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
		}
	}


	

