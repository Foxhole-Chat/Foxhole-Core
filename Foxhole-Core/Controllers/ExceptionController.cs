using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foxhole_Core.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ExceptionController : ControllerBase
	{
		[HttpGet("404")]
		public IEnumerable<string> PageNotFound()
		{
			return new string[] { "value1", "value2" };
		}
	}
}