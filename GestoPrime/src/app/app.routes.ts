import { Routes } from '@angular/router';
import { loginGuard } from './core/guards/login.guard';
import { dashGuard } from './core/guards/dash.guard';
import { adminGuard } from './core/guards/admin.guard';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { StatistiquesComponent } from './features/dashboard/Administration/statistiques/statistiques.component';
import { MouvementsComponent } from './features/dashboard/Administration/mouvements/mouvements.component';
import { ParammetresAccesComponent } from './features/dashboard/Administration/parammetres-acces/parammetres-acces.component';
import { AjoutComponent } from './features/dashboard/Administration/parammetres-acces/ajout/ajout.component';
import { LoginComponent } from './features/login/login.component';
import { MajSalarieComponent } from './features/dashboard/Gestion/maj-salarie/maj-salarie.component';
import { MajScoreComponent } from './features/dashboard/Gestion/maj-score/maj-score.component';
import { DroitsPrimesComponent } from './features/dashboard/droits-primes/droits-primes.component';


export const routes: Routes = [
  
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [dashGuard],
    children: [
      { path: '', redirectTo: 'statistics', pathMatch: 'full' },
      
      // --- Section Administration ---
      { path: 'statistics', component: StatistiquesComponent },
      { path: 'mouvements', component: MouvementsComponent },
      { 
        path: 'access', 
        component: ParammetresAccesComponent, 
        children: [
          { path: 'ajout', component: AjoutComponent }
        ]
      },

      // --- Section Gestion (Ajoutée ici) ---
      { 
        path: 'gestion', 
        children: [
          { path: 'maj-salarie', component: MajSalarieComponent },
          { path: 'maj-score', component: MajScoreComponent },
          { path: 'droits-primes', component: DroitsPrimesComponent }
        ]
      }
    ]
  },
  { path: 'login', canActivate: [loginGuard], component: LoginComponent},

  { path: '**', redirectTo: 'dashboard' }
];