import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../../core/api';
import { Movimentacao, MovimentacaoRequest, Produto } from './estoque.models';

@Injectable({ providedIn: 'root' })
export class EstoqueService {
  private readonly http = inject(HttpClient);

  listarProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${API_URL}/produtos`);
  }

  listarMovimentacoes(): Observable<Movimentacao[]> {
    return this.http.get<Movimentacao[]>(`${API_URL}/estoque/movimentacoes`);
  }

  movimentar(request: MovimentacaoRequest): Observable<Movimentacao> {
    return this.http.post<Movimentacao>(`${API_URL}/estoque/movimentacoes`, request);
  }
}
