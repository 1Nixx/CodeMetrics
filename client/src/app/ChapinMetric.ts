import * as internal from "stream";

export interface ChapinTypesVariable {
  P: string[];
  M: string[];
  C: string[];
  T: string[];
}

export interface ChapinTypesCount {
  P: Number;
  M: Number;
  C: Number;
  T: Number;
}

export interface IChapinMetric {
  chapinTypes: ChapinTypesVariable;
  variableCount:  ChapinTypesCount;
  chapinIOTypes: ChapinTypesVariable;
  variableIOCount: ChapinTypesCount;
  metricResult: Number;
  metricIOResult: Number;
}
