using Halstead.ProgramAnalyzer;
using Metrics.Halstead;
using Metrics.ProgramAnalyzer;
using Metrics.ReturnData;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HalsteadController : ControllerBase
	{
		public HalsteadController()
		{
		}

		[HttpPost]
		public async Task<ActionResult<HalsteadMetricReturn>> GetMetric()
		{
			using (var reader = new StreamReader(Request.Body))
			{
				string plainText = await reader.ReadToEndAsync();
				var a = new Lexer(plainText);
				var items = new Parcer(a.TokenizeCode()).Parce();
				HalsteadMetric metric = new HalsteadMetric(items);
				return Ok(metric.CreateListsOfOperandsAndOperators());
			}			
		}
	}
}
