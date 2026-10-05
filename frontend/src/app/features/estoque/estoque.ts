import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { mensagemDeErro } from '../../core/api';
import { Movimentacao, MovimentacaoRequest, Produto, TipoMovimentacao } from './estoque.models';
import { EstoqueService } from './estoque.service';

@Component({
  selector: 'app-estoque',
  imports: [ReactiveFormsModule, DatePipe],
  templateUrl: './estoque.html',
})
export class Estoque {
  private readonly service = inject(EstoqueService);
  private readonly fb = inject(FormBuilder);

  protected readonly produtos = signal<Produto[]>([]);
  protected readonly historico = signal<Movimentacao[]>([]);
  protected readonly ultima = signal<Movimentacao | null>(null);
  protected readonly erro = signal('');
  protected readonly enviando = signal(false);

  protected readonly form = this.fb.nonNullable.group({
    codigoProduto: [0, [Validators.required, Validators.min(1)]],
    tipo: ['Entrada' as TipoMovimentacao, Validators.required],
    descricao: ['', [Validators.required, Validators.maxLength(200)]],
    quantidade: [1, [Validators.required, Validators.min(1)]],
  });

  constructor() {
    this.atualizar();
  }

  protected registrar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.erro.set('');
    this.enviando.set(true);

    const request: MovimentacaoRequest = this.form.getRawValue();

    this.service.movimentar(request).subscribe({
      next: (movimentacao) => {
        this.ultima.set(movimentacao);
        this.form.patchValue({ descricao: '', quantidade: 1 });
        this.form.controls.descricao.markAsUntouched();
        this.enviando.set(false);
        this.atualizar();
      },
      error: (e) => {
        this.ultima.set(null);
        this.erro.set(mensagemDeErro(e));
        this.enviando.set(false);
      },
    });
  }

  private atualizar(): void {
    this.service.listarProdutos().subscribe({
      next: (lista) => this.produtos.set(lista),
      error: (e) => this.erro.set(mensagemDeErro(e)),
    });
    this.service.listarMovimentacoes().subscribe({
      next: (lista) => this.historico.set(lista),
      error: (e) => this.erro.set(mensagemDeErro(e)),
    });
  }
}
