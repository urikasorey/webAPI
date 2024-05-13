﻿using libaryAPI.Models;
using System.ComponentModel.DataAnnotations;
namespace libaryAPI.Models.DTO
{
	public class AddBookRequestDTO
	{
		[Required]
		[MinLength (10)]
		public string? Title { get; set; }
		public string ?Description { get; set; }
		public bool isRead { get; set; }
		public DateTime? DateRead { get; set; }
		public int? Rate { get; set; }
		public string? Genre { get; set; }
		public string? CoverUrl { get; set; }
		public DateTime DateAdded { get; set; }
		public int PublisherID { get; set; }
		public List<int> AuthorIDs { get; set; }
	}
}
