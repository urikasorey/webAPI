using System.ComponentModel.DataAnnotations;

/*namespace libaryAPI.Models.DTO
{
	public class ImageUploadRequestDTO
	{
		[Required]
		public IFormFile File { get; set; }
		[Required]
		public string FileName { get; set; }
		public string? FileDescription { get; set; }
		private void ValidateFileUpload(ImageUploadRequestDTO requestDTO)
		{
			var allowExtensions = new string[] { ".jpg", ".png", ".jpeg" };
			if (!allowExtensions.Contains(Path.GetExtension(requestDTO.File.FileName)))
			{
				ModelState.AddModelError("File", "Invalid file extension. Only .jpg, .png, .jpeg are allowed");
			}
			if (requestDTO.File.Length > 1040000)
			{
				ModelState.AddModelError("File", "File size cannot exceed <10MB");
			}
		}

	}
}
*/