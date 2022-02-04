using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foxhole_Core.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ExceptionController : ControllerBase
	{
		[HttpGet]
		public IEnumerable<string> Get(int id)
		{
			return new string[] { "value1", "value2" };
		}
	}
}