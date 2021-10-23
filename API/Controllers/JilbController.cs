using Metrics.Jilb;
using Metrics.ReturnData;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace HelsteadMetricAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JilbController : ControllerBase
	{

		[HttpPost]
		public async Task<ActionResult<JilbMetricReturn>> GetMetric()
		{
			using (var reader = new StreamReader(Request.Body))
			{
				string plainText = await reader.ReadToEndAsync();
				var metricResult = new JilbMetric(plainText);
				return Ok(await metricResult.CalculateMetricAsync());
			}
		}
	}
}
