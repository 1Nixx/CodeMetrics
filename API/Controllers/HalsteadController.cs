using Halstead.ProgramAnalyzer;
using Metrics.Halstead;
using Metrics.ProgramAnalyzer;
using Metrics.ReturnData;
using Metrics.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HalstedMetricParcer = Halstead.ProgramAnalyzer;

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
				var tokens = (new Lexer(plainText)).TokenizeCode();
				var operatorsParcer = new HalstedMetricParcer.Parcer(new ParcerHalstead(tokens)).parcedCode;
				HalsteadMetric metric = new HalsteadMetric(operatorsParcer);
				return Ok(metric.CreateListsOfOperandsAndOperators());
			}			
		}
	}
}
