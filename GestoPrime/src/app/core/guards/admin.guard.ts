import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const adminGuard: CanActivateFn = (route, state) => {
 const router = inject(Router);
  const role = localStorage.getItem('role'); // Récupéré lors du login

  if (role === 'Admin') {
    return true;
  }

  // Redirection amicale vers les statistiques si non autorisé
  router.navigate(['/dashboard/mouvements']);
  return false;
};
