import { IOperandsAndOperators } from "./oper";

export interface IMetric
{
  listOfOperands: IOperandsAndOperators[];
  listOfOperators: IOperandsAndOperators[];
  numOfUniqueOperands: number;
  numOfUniqueOperators: number;
  totalNumOfOperands: number;
  totalNumOfOperators: number;
  dictionaryOfProgram: number;
  lenOfProgram: number;
  volumeOfProgram: number;
}
