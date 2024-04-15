using Microsoft.AspNetCore.Mvc;

namespace libaryAPI.Controllers
{
	public class BookController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
