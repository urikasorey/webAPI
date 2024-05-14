using libaryAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*namespace libaryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository _imageRepository;
		public ImagesController(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}
		[HttpPost]
		[Route("upload-image")]
		public IActionResult UploadImage([FromForm] ImageUpLoadRequestDTO requestDTO)
		{
			ValidateFileUpload(requestDTO);
			if (!ModelState.IsValid)
			{
				// convert DTO to Domain model
				var imageDomain = new Image
				{
					File = requestDTO.File,
					FileExtenSion = Path.GetExtension(requestDTO.File.FileName),
					FileSizeInByte = requestDTO.File.Length,
					FileName = requestDTO.FileName,
					FileDescription = requestDTO.FileDescription
				};
				//	User repository to upload image
				_imageRepository.ImageUpload(imageDomain);
				return Ok(imageDomain);
			}
			return BadRequest(ModelState);
		}
		[HttpGet]
		public IActionResult GetAllInfoImgaes()
		{
			var allimages = _imageRepository.GetAllInfoImages();
			return Ok(allimages);
		}
		[HttpGet]
		[Route("Dowload")]
		public IActionResult DowloadImage(int id);
		{
			var result = _imageRepository.DowloadImage(id);
			return File(result.Item1, result.Item2, result.Item3);
		}
	}
}

*/