﻿using libaryAPI.Data;
using libaryAPI.Models;
using libaryAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace libaryAPI.Repository
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly dbcontext _dbContext;
		public AuthorRepository(dbcontext dbcontext)
		{
			_dbContext = dbcontext;
		}
		public List<AuthorDTO> GellAllAuthors()
		{
			//Get Data From Database -Domain Model 
			var allAuthorsDomain = _dbContext.Authors.ToList();
			//Map domain models to DTOs 
			var allAuthorDTO = new List<AuthorDTO>();
			foreach (var authorDomain in allAuthorsDomain)
			{
				allAuthorDTO.Add(new AuthorDTO()
				{
					Id = authorDomain.AuthorID,
					FullName = authorDomain.FullName
				});
			}
			//return DTOs 
			return allAuthorDTO;
		}
		public AuthorNoIdDTO GetAuthorById(int id)
		{
			// get book Domain model from Db 
			var authorWithIdDomain = _dbContext.Authors.FirstOrDefault(x => x.AuthorID ==
id);
			if (authorWithIdDomain == null)
			{
				return null;
			}
			//Map Domain Model to DTOs 
			var authorNoIdDTO = new AuthorNoIdDTO
			{
				FullName = authorWithIdDomain.FullName,
			};
			return authorNoIdDTO;
		}
		public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorDomainModel = new Authors
			{
				FullName = addAuthorRequestDTO.FullName,
			};
			//Use Domain Model to create Author 
			_dbContext.Authors.Add(authorDomainModel);
			_dbContext.SaveChanges();
			return addAuthorRequestDTO;
		}
		public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
		{
			var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.AuthorID == id);
			if (authorDomain != null)
			{
				authorDomain.FullName = authorNoIdDTO.FullName;
				_dbContext.SaveChanges();
			}
			return authorNoIdDTO;
		}

		public Authors? DeleteAuthorById(int id)
		{
			var authorDomain = _dbContext.Authors.FirstOrDefault(n => n.AuthorID == id);
			if (authorDomain != null)
			{
					_dbContext.Authors.Remove(authorDomain);
				_dbContext.SaveChanges();
			}
			return null;
		}
	}
}