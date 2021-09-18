using Lab1ConsoleProg.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1ConsoleProg
{
	class HalsteadMetric
	{
		private readonly List<ProgramEntity> programEntities;

		public List<OperandsAndOperators> ListOfOperands = new List<OperandsAndOperators>();
		public List<OperandsAndOperators> ListOfOperators = new List<OperandsAndOperators>();

		public HalsteadMetric(IEnumerable<ProgramEntity> programEntities)
		{
			this.programEntities = (List<ProgramEntity>)programEntities;
		}

		public HalsteadMetricReturn CreateListsOfOperandsAndOperators()
		{	
			var ExpressionGroups = from ProgramEntity in programEntities
								   group ProgramEntity by ProgramEntity.Value into g
								   select new
								   {
									   Value = g.Key,
									   Count = g.Count(),
									   programEntities = from p in g select p
								   };


			foreach (var group in ExpressionGroups)
			{
				foreach (ProgramEntity ProgramEntity in group.programEntities)
				{
					if (ProgramType.Operand == ProgramEntity.Type)
					{
						ListOfOperands.Add(new OperandsAndOperators() { Value = group.Value, NumOfRep = group.Count });
					}
					else
					{
						ListOfOperators.Add(new OperandsAndOperators() { Value = group.Value, NumOfRep = group.Count });
					}
					break;
				}
			}

			// NumOfUniqueOperands - число уникальных операндов программы
			// NumOfUniqueOperators - число уникальных операторов программы
			// TotalNumOfOperands - общее число операндов в программе
			// TotalNumOfOperators - общее число операторов в программе
			// DictionaryOfProgram = NumOfUniqueOperands + NumOfUniqueOperators - словарь программы
			// LenOfProgram = TotalNumOfOperands + TotalNumOfOperators - длина программы
			// VolumeOfProgram = LenOfProgram * log2(DictionaryOfProgram)(бит) - объём программы

			int NumOfUniqueOperands = GetNumOfUniqueOperands(ListOfOperands);
			int NumOfUniqueOperators = GetNumOfUniqueOperators(ListOfOperators);
			int TotalNumOfOperands = GetTotalNumOfOperands(ListOfOperands);
			int TotalNumOfOperators = GetTotalNumOfOperators(ListOfOperators);
			int DictionaryOfProgram = NumOfUniqueOperands + NumOfUniqueOperators;
			int LenOfProgram = TotalNumOfOperands + TotalNumOfOperators;
			int VolumeOfProgram = (int)(LenOfProgram * Math.Log(DictionaryOfProgram, 2));

			static int GetNumOfUniqueOperands(List<OperandsAndOperators> NumOfUniqueOperands)
			{
				int count = NumOfUniqueOperands.Count;
				return count;
			}

			static int GetNumOfUniqueOperators(List<OperandsAndOperators> NumOfUniqueOperators)
			{
				int count = NumOfUniqueOperators.Count;
				return count;
			}

			static int GetTotalNumOfOperands(List<OperandsAndOperators> TotalNumOfOperands)
			{
				int count = 0;
				foreach (OperandsAndOperators p in TotalNumOfOperands)
				{
					count += p.NumOfRep;
				}
				return count;
			}

			static int GetTotalNumOfOperators(List<OperandsAndOperators> TotalNumOfOperators)
			{
				int count = 0;
				foreach (OperandsAndOperators p in TotalNumOfOperators)
				{
					count += p.NumOfRep;
				}
				return count;
			}

			return new HalsteadMetricReturn { ListOfOperands = ListOfOperands, ListOfOperators = ListOfOperators, NumOfUniqueOperands = NumOfUniqueOperands, NumOfUniqueOperators = NumOfUniqueOperators, TotalNumOfOperands = TotalNumOfOperands, TotalNumOfOperators = TotalNumOfOperators, DictionaryOfProgram = DictionaryOfProgram, LenOfProgram = LenOfProgram, VolumeOfProgram = VolumeOfProgram };

	}

	}

	class OperandsAndOperators
	{
		public string Value { get; set; }
		public int NumOfRep { get; set; }

	}

	class HalsteadMetricReturn
	{
		public List<OperandsAndOperators> ListOfOperands { get; set; }
		public List<OperandsAndOperators> ListOfOperators { get; set; }
		public int NumOfUniqueOperands { get; set; }
		public int NumOfUniqueOperators { get; set; }
		public int TotalNumOfOperands { get; set; }
		public int TotalNumOfOperators { get; set; }
		public int DictionaryOfProgram { get; set; }
		public int LenOfProgram { get; set; }
		public int VolumeOfProgram { get; set; }

	}
}
