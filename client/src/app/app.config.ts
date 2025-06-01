import { APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './core/interceports/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { lastValueFrom } from 'rxjs';
import { InitService } from './core/services/init.service';
import { authInterceptor } from './core/interceptors/auth.interceptor';

function initializeApp(InitService: InitService) {
  return () => lastValueFrom(InitService.Init()).finally(() => {
    const splash = document.getElementById('initial-splash');
    if (splash) {
      splash.remove();
    }
  })
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(),
    provideHttpClient(withInterceptors([
      errorInterceptor, 
      loadingInterceptor,
      authInterceptor
    ])),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      multi: true,
      deps: [InitService]
    }

  ]
};
