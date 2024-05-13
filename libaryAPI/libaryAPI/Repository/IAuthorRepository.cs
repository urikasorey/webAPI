using libaryAPI.Models;
using libaryAPI.Models.DTO;
namespace libaryAPI.Repository
{
	public interface IAuthorRepository
	{
		List<AuthorDTO> GellAllAuthors();
		AuthorNoIdDTO GetAuthorById(int id);
		AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
		AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
		Authors? DeleteAuthorById(int id);
	}
}