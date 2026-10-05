export type TipoMovimentacao = 'Entrada' | 'Saida';

export interface Produto {
  codigo: number;
  descricao: string;
  estoque: number;
}

export interface MovimentacaoRequest {
  codigoProduto: number;
  tipo: TipoMovimentacao;
  descricao: string;
  quantidade: number;
}

export interface Movimentacao {
  id: number;
  descricaoProduto: string;
  tipo: TipoMovimentacao;
  descricao: string;
  quantidade: number;
  dataHora: string;
  saldoFinal: number;
}
