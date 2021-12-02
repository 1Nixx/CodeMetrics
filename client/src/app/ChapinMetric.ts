export interface ChapinTypes {
  P: string[];
  M: string[];
  C: string[];
  T: string[];
}

export interface ChapinIOTypes {
  P: any[];
  M: any[];
  C: string[];
  T: any[];
}

export interface IChapinMetric {
  chapinTypes: ChapinTypes;
  chapinIOTypes: ChapinIOTypes;
}
