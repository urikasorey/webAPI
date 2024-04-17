using Microsoft.EntityFrameworkCore;
using libaryAPI.Models;

namespace libaryAPI.Data
{
	public class dbcontext:DbContext
	{
		public dbcontext(DbContextOptions<dbcontext> options) : base(options) 
		{

		}
		/*protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book_Author>()
				.HasOne(b => b.Books)
				.WithMany(ba => ba.Book_author)
				.HasForeignKey(Bi => bi.BookID);
				modelbuider.
		}*/
		public DbSet<Books>Books { get; set; }
		public DbSet<Authors> Authors { get; set; }
		public DbSet<Book_Author>Book_Author { get; set; }
		public DbSet<Publishers>Publishers { get; set; }

		internal async Task GetBooksAsync(int iD)
		{
			throw new NotImplementedException();
		}
	}	
}
