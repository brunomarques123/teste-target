import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import localePt from '@angular/common/locales/pt';
import { routes } from './app.routes';

// Locale pt-BR para os pipes de moeda, data e porcentagem.
registerLocaleData(localePt);

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withFetch()),
    { provide: LOCALE_ID, useValue: 'pt-BR' },
  ],
};
