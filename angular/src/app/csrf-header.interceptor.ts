
import {
  HttpRequest,
  HttpEvent,
  HttpHandlerFn
} from '@angular/common/http';
import { Observable } from 'rxjs';

export function xsrfHeaderInterceptor(request: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
  if (!request.headers.has("X-CSRF")) {
    request = request.clone({
      headers: request.headers.set("X-CSRF", "1"),
    });
  }

  return next(request);
}