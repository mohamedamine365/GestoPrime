import { HttpInterceptorFn } from '@angular/common/http';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
 const token = localStorage.getItem('token');

  // On ne clone et modifie la requête que si le token existe
  if (token) {
    const request = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}` // Utilisation des backticks pour plus de clarté
      }
    });
    return next(request);
  }

  // Si pas de token, on laisse passer la requête originale (ex: Login)
  return next(req);;
};
