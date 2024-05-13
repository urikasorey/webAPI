using libaryAPI.Data;
using libaryAPI.Models.DTO;
using libaryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace libaryAPI.Repository
{
	public class PublisherRepository : IPublisherRepository
	{
		private readonly dbcontext _dbcontext;
		public PublisherRepository(dbcontext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public List<PublisherDTO> GetAllPublishers()
		{
			//Get Data From Database -Domain Model 
			var allPublishersDomain = _dbcontext.Publishers.ToList();

			//Map domain models to DTOs 
			var allPublisherDTO = new List<PublisherDTO>();
			foreach (var publisherDomain in allPublishersDomain)
			{
				allPublisherDTO.Add(new PublisherDTO()
				{
					ID = publisherDomain.ID,
					Name = publisherDomain.Name

				});
			}
			return allPublisherDTO;
		}

		public PublisherNoIdDTO GetPublisherById(int id)
		{
			// get book Domain model from Db 
			var publisherWithIdDomain = _dbcontext.Publishers.FirstOrDefault(x => x.ID ==
id);
			if (publisherWithIdDomain != null)
			{ //Map Domain Model to DTOs 
				var publisherNoIdDTO = new PublisherNoIdDTO
				{
					Name = publisherWithIdDomain.Name,
				};

				return publisherNoIdDTO;
			}

			return null;

		}
		public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO
addPublisherRequestDTO)
		{
			var publisherDomainModel = new Publishers
			{
				Name = addPublisherRequestDTO.Name,
			};
			//Use Domain Model to create Book 
			_dbcontext.Publishers.Add(publisherDomainModel);
			_dbcontext.SaveChanges();
			return addPublisherRequestDTO;
		}

		public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
		{
			var publisherDomain = _dbcontext.Publishers.FirstOrDefault(n => n.ID == id);
			if (publisherDomain != null)
			{
				publisherDomain.Name = publisherNoIdDTO.Name;

				_dbcontext.SaveChanges();
			}
			return null;
		}
		public Publishers? DeletePublisherById(int id)
		{
			var publisherDomain = _dbcontext.Publishers.FirstOrDefault(n => n.ID == id);
			if (publisherDomain != null)
			{
				_dbcontext.Publishers.Remove(publisherDomain);
				_dbcontext.SaveChanges();
			}
			return null;
		}
	}
}