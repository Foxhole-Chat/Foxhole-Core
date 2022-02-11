using Microsoft.AspNetCore.Mvc;

namespace Foxhole_Core.Controllers
{
	[Route("/")]
	[Route("[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "This is a placeholder btw", "value2" };
		}
	}
}