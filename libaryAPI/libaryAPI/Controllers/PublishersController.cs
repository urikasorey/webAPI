using libaryAPI.Data;
using libaryAPI.Models;
using libaryAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{

		private readonly dbcontext dbcontext1;
		public PublishersController(dbcontext dbcontext)
		{
			dbcontext1 = dbcontext;
		}
		//get all publishers
		[HttpGet("get-all-publishers")]
		public IActionResult GetAll()
		{
			var allPublishersDomain = dbcontext1.Publishers;
			//map domain models to DTO
			var allPublishersDTO = allPublishersDomain.Select(Publishers => new PublisherDTO()
			{
				ID = Publishers.ID,
				Name = Publishers.Name,
				Books = Publishers.Books.Select(n => new Books()
				{
					ID = n.ID,
					Title = n.Title,
					Description = n.Description,
					isRead = n.isRead,
					DateRead = n.DateRead,
					Rate = n.Rate,
					Genre = n.Genre,
					CoverUrl = n.CoverUrl,
					DateAdded = n.DateAdded,
					PublishersID = n.PublishersID,
					
				}).ToList()
			}).ToList();
			return Ok(allPublishersDTO);
		}
		//get publisher by id
		[HttpGet(" get-Publisher-by-Id{id}")]
		public IActionResult GetPublisherId([FromRoute] int id)
		{
			//get publisher domain model from db
			var publisher = dbcontext1.Publishers.FirstOrDefault(n => n.ID == id);
			if (publisher == null)
			{
				return NotFound();
			}
			//map domain model to DTO
			var publisherDTO = new PublisherDTO()
			{
				ID = publisher.ID,
				Name = publisher.Name,
				Books = publisher.Books.Select(n => new Books()
				{
					ID = n.ID,
					Title = n.Title,
					Description = n.Description,
					isRead = n.isRead,
					DateRead = n.DateRead,
					Rate = n.Rate,
					Genre = n.Genre,
					CoverUrl = n.CoverUrl,
					DateAdded = n.DateAdded,
					PublishersID = n.PublishersID,
					
				}).ToList()
			};return Ok();
		}

	}
}
