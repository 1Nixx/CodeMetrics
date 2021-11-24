using Metrics.ChapinMetric;
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
	public class ChapinController : ControllerBase
	{
		[HttpPost]
		public async Task<ChapinMetricReturn> GetMetric()
		{
			using (var reader = new StreamReader(Request.Body))
			{
				string plainText = await reader.ReadToEndAsync();
				var metricResult = new ChapinMetric(plainText);
				var result = metricResult.CalculateChapinMetric();
				return result;
			}
		}
	}
}
