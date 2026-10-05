import { CurrencyPipe, DatePipe, PercentPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { mensagemDeErro } from '../../core/api';
import { JurosResponse } from './juros.models';
import { JurosService } from './juros.service';

@Component({
  selector: 'app-juros',
  imports: [ReactiveFormsModule, CurrencyPipe, DatePipe, PercentPipe],
  templateUrl: './juros.html',
})
export class Juros {
  private readonly service = inject(JurosService);
  private readonly fb = inject(FormBuilder);

  protected readonly resultado = signal<JurosResponse | null>(null);
  protected readonly erro = signal('');
  protected readonly enviando = signal(false);

  protected readonly form = this.fb.nonNullable.group({
    valor: [null as number | null, [Validators.required, Validators.min(0.01)]],
    vencimento: ['', Validators.required],
  });

  protected calcular(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.erro.set('');
    this.enviando.set(true);

    const { valor, vencimento } = this.form.getRawValue();

    this.service.calcular({ valor: Number(valor), vencimento }).subscribe({
      next: (resposta) => {
        this.resultado.set(resposta);
        this.enviando.set(false);
      },
      error: (e) => {
        this.resultado.set(null);
        this.erro.set(mensagemDeErro(e));
        this.enviando.set(false);
      },
    });
  }
}
