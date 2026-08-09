import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideToastr } from 'ngx-toastr';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './_interceptors/error-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideToastr({
      positionClass: 'toast-bottom-right'
    }),
    provideHttpClient(
      withInterceptors([errorInterceptor])
    )
  ]
};
