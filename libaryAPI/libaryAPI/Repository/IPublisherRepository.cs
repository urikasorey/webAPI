using libaryAPI.Models.DTO;
using libaryAPI.Models;

namespace libaryAPI.Repository
{
	public interface IPublisherRepository
	{
		 
        List<PublisherDTO> GetAllPublishers();
		PublisherNoIdDTO GetPublisherById(int id);
		AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
		PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
		Publishers? DeletePublisherById(int id);
	}
}

