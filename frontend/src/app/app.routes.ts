import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'comissoes' },
  {
    path: 'comissoes',
    title: 'Comissões',
    loadComponent: () => import('./features/comissoes/comissoes').then((m) => m.Comissoes),
  },
  {
    path: 'estoque',
    title: 'Estoque',
    loadComponent: () => import('./features/estoque/estoque').then((m) => m.Estoque),
  },
  {
    path: 'juros',
    title: 'Juros',
    loadComponent: () => import('./features/juros/juros').then((m) => m.Juros),
  },
  { path: '**', redirectTo: 'comissoes' },
];
