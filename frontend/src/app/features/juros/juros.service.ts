import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../../core/api';
import { JurosRequest, JurosResponse } from './juros.models';

@Injectable({ providedIn: 'root' })
export class JurosService {
  private readonly http = inject(HttpClient);

  calcular(request: JurosRequest): Observable<JurosResponse> {
    return this.http.post<JurosResponse>(`${API_URL}/juros/calcular`, request);
  }
}
