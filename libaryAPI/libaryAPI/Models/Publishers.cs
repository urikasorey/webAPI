using System.ComponentModel.DataAnnotations;

namespace libaryAPI.Models
{
	public class Publishers
	{
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Books>Books { get; set; }
	}
}
