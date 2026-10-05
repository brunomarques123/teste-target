import { CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { mensagemDeErro } from '../../core/api';
import { ComissaoResponse } from './comissoes.models';
import { ComissoesService } from './comissoes.service';

@Component({
  selector: 'app-comissoes',
  imports: [CurrencyPipe],
  templateUrl: './comissoes.html',
})
export class Comissoes {
  private readonly service = inject(ComissoesService);

  protected readonly resultado = signal<ComissaoResponse | null>(null);
  protected readonly erro = signal('');
  protected readonly carregando = signal(false);

  protected calcular(): void {
    this.erro.set('');
    this.carregando.set(true);

    this.service.calcular().subscribe({
      next: (resposta) => {
        this.resultado.set(resposta);
        this.carregando.set(false);
      },
      error: (e) => {
        this.resultado.set(null);
        this.erro.set(mensagemDeErro(e));
        this.carregando.set(false);
      },
    });
  }
}
