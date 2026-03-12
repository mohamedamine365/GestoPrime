import { Routes } from '@angular/router';
import { loginGuard } from './core/guards/login.guard';
import { dashGuard } from './core/guards/dash.guard';
import { adminGuard } from './core/guards/admin.guard';

import { DashboardComponent } from './features/Administration/dashboard/dashboard.component';
import { LoginComponent } from './features/login/login.component';
import { StatistiquesComponent } from './features/Administration/dashboard/statistiques/statistiques.component';
import { MouvementsComponent } from './features/Administration/dashboard/mouvements/mouvements.component';
import { ParammetresAccesComponent } from './features/Administration/dashboard/parammetres-acces/parammetres-acces.component';
import { AjoutComponent } from './features/Administration/dashboard/parammetres-acces/ajout/ajout.component';

export const routes: Routes = [
  
  { path: '', redirectTo: 'login', pathMatch: 'full' },

  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [dashGuard],
    children: [
      { path: '', redirectTo: 'statistics', pathMatch: 'full' },
      
   
      { path: 'statistics', component: StatistiquesComponent },
      
      
      { path: 'mouvements', component: MouvementsComponent },
      
      
      { 
        path: 'access', 
        component: ParammetresAccesComponent, 
        children: [
          
          { path: 'ajout', component: AjoutComponent }
        ]
      }
    ]
  },
  { path: 'login', canActivate: [loginGuard], component: LoginComponent},

  { path: '**', redirectTo: 'dashboard' }
];