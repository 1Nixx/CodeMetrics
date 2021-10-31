import { IOperandsAndOperators } from "./oper";

export interface IJilbMetric 
{
    amountOfConditionalOperators: Number;
    relativeComplexityOfProgram: Number;
    maximumNestingLevel: Number;
    listOfOperators: IOperandsAndOperators[];
}