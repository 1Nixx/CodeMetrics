import { IOperandsAndOperators } from "./oper";

export interface IHalsteadMetric
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
