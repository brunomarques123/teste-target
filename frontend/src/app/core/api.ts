import { HttpErrorResponse } from '@angular/common/http';

export const API_URL = 'http://localhost:5086/api';

// Extrai a mensagem do ProblemDetails devolvido pela API.
export function mensagemDeErro(erro: unknown): string {
  if (!(erro instanceof HttpErrorResponse)) {
    return 'Erro inesperado. Tente novamente.';
  }
  if (erro.status === 0) {
    return 'Não foi possível conectar à API. Verifique se ela está rodando.';
  }
  if (erro.error?.errors) {
    return Object.values<string[]>(erro.error.errors).flat().join(' ');
  }
  return erro.error?.detail ?? erro.error?.title ?? 'Erro inesperado. Tente novamente.';
}
