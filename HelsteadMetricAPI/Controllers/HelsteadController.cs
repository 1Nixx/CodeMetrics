using Lab1ConsoleProg;
using Lab1ConsoleProg.ProgramAnalyzer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelsteadMetricAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HelsteadController : ControllerBase
	{
		public HelsteadController()
		{
		}

		[HttpGet]
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
