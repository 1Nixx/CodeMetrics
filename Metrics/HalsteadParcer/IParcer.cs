using Halstead.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metrics.Halstead
{
	public interface IParcer
	{
		public List<ProgramEntity> Parce();
	}
}
