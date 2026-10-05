export interface JurosRequest {
  valor: number;
  vencimento: string; // yyyy-MM-dd
}

export interface JurosResponse {
  valor: number;
  vencimento: string;
  dataCalculo: string;
  diasAtraso: number;
  taxaDiaria: number;
  juros: number;
  valorTotal: number;
}
