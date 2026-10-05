export interface ResumoVendedor {
  vendedor: string;
  quantidadeVendas: number;
  totalVendido: number;
  totalComissao: number;
}

export interface ComissaoResponse {
  vendedores: ResumoVendedor[];
  totalVendido: number;
  totalComissao: number;
}
