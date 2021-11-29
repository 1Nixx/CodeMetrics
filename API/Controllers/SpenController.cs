using Metrics;
using Metrics.SpenMetric;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpenController : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<SpenMetricReturn>> GetMetric()
		{
			using (var reader = new StreamReader(Request.Body))
			{
				string plainText = await reader.ReadToEndAsync();
				var metricResult = new SpenMetric(plainText);
				return Ok(metricResult.CalculateSpenMetric());
			}
		}
	}
}
