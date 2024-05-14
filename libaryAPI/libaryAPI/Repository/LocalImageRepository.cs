using libaryAPI.Data;
using libaryAPI.Models;
using System.Linq.Expressions;

/*namespace libaryAPI.Repository
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment _webHostingEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly dbcontext _dbcontext;
		public LocalImageRepository(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, dbcontext dbcontext)
		{
			_webHostingEnvironment = hostingEnvironment;
			_httpContextAccessor = httpContextAccessor;
			_dbcontext = dbcontext;
		}
		public Image Upload(Image image)
		{
			var localFilePath = Path.Combine(_webHostingEnvironment.ContentRootPath, "Images",
				$"{image.FileName}{image.FileExtension}");
			//upload Image to local path
			using var steam = new FileStream(localFilePath, FileMode.Create);
			image.File.CopyTo(steam);

			//htpps://localhost:5001/Images/fileName.jpg
			var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/{image.FileName}{image.FileExtension}";
			image.FilePath = urlFilePath;

			//add Image to Images table
			_dbcontext.Images.Add(image);
			_dbcontext.SaveChanges();
			return image;
		}
		public List<Image> GetAllInfoImages()
		{
			var allImages = _dbcontext.Images.ToList();
			return allImages;
		}
		public (byte[], string, string) DowloadImage(string Id)
		{
			var image = _dbcontext.Images.Where(x => x.Id == Id);
			var localFilePath = Path.Combine(_webHostingEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
			var steam = File.ReadAllBytes(localFilePath);
			var fileName = FileById.FileName + FileById.FileExtension;
			return (steam, "application/octet-steam", fileName);
		}
		Catch(Exception ex)
			{
			throw ex;
			}
	}
}
*/