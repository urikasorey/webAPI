using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libaryAPI.Models
{
	public class Books
	{
		[Key]
		public int ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool isRead { get; set; }
		public DateTime? DateRead { get; set; }
		public int? Rate { get; set; }
		public int Genre { get; set; }
		public string CoverUrl { get; set; }
		public DateTime DateAdded { get; set; }
	
		public string Publishersid {get; set; }
		public Publishers Publishers { get; set; }
		public List<Book_Author> Book_Authors { get; set; }
		
	}
}
