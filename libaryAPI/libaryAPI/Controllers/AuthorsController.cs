using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using libaryAPI.Repository;
using libaryAPI.Models;
using libaryAPI.Data;
using libaryAPI.Models.DTO;
using static libaryAPI.Models.DTO.AuthorDTO;
using System;
namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController : ControllerBase
	{
		private readonly dbcontext _dbContext;
		private readonly IAuthorRepository _authorRepository;
		public AuthorsController(dbcontext dbContext, IAuthorRepository
authorRepository)
		{
			_dbContext = dbContext;
			_authorRepository = authorRepository;
		}

		[HttpGet("get-all-author")]
		public IActionResult GetAllAuthor()
		{
			var allAuthors = _authorRepository.GellAllAuthors();
			return Ok(allAuthors);
		}

		[HttpGet("get-author-by-id/{id}")]
		public IActionResult GetAuthorById(int id)
		{
			var authorWithId = _authorRepository.GetAuthorById(id);
			return Ok(authorWithId);
		}

		[HttpPost("add-Author")]
		public IActionResult AddAuthors([FromBody] AddAuthorRequestDTO
addAuthorRequestDTO)
		{
			var authorAdd = _authorRepository.AddAuthor(addAuthorRequestDTO);
			return Ok();
		}

		[HttpPut("update-author-by-id/{id}")]
		public IActionResult UpdateBookById(int id, [FromBody] AuthorNoIdDTO
authorDTO)
		{
			var authorUpdate = _authorRepository.UpdateAuthorById(id, authorDTO);
			return Ok(authorUpdate);
		}

		[HttpDelete("delete-author-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			var authorDelete = _authorRepository.DeleteAuthorById(id);
			return Ok();
		}
	}
}
