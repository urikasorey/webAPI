using libaryAPI.Models;
using libaryAPI.Models.DTO;
using static libaryAPI.Models.DTO.AuthorDTO;

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
