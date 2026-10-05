import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../../core/api';
import { ComissaoResponse } from './comissoes.models';

@Injectable({ providedIn: 'root' })
export class ComissoesService {
  private readonly http = inject(HttpClient);

  calcular(): Observable<ComissaoResponse> {
    return this.http.get<ComissaoResponse>(`${API_URL}/comissoes`);
  }
}
