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

import { TauxPrimesComponent } from './features/dashboard/Gestion/taux-primes/taux-primes.component';
import { PeriodeComponent } from './features/dashboard/Gestion/periode/periode.component';
import { MajParamComponent } from './features/dashboard/Gestion/maj-param/maj-param.component';
import { PointageComponent } from './features/dashboard/Consultation/pointage/pointage.component';
import { ConsultationSalarieComponent } from './features/dashboard/Consultation/consultation-salarie/consultation-salarie.component';
import { DroitsPrimesComponent } from './features/dashboard/Gestion/droits-primes/droits-primes.component';
import { PlafondPrimeRendementComponent } from './features/dashboard/Controle/plafond-prime-rendement/plafond-prime-rendement/plafond-prime-rendement.component';
import { IndemniteDeplacementComponent } from './features/dashboard/Controle/indemnite-deplacement/indemnite-deplacement/indemnite-deplacement.component';
import { AvancePrimeComponent } from './features/dashboard/Controle/avance-prime/avance-prime.component';
import { UniteGestionnaireComponent } from './features/dashboard/Controle/unite-gestionnaire/unite-gestionnaire.component';
import { ControlePointageComponent } from './features/dashboard/Controle/controle-pointage/controle-pointage.component';
import { ControlDroitsPrimesComponent } from './features/dashboard/Controle/droits-primes/droits-primes.component';
import { ControleTauxPrimesComponent } from './features/dashboard/Controle/taux-primes/taux-primes.component';
import { ConsultPlafondPrimeComponent } from './features/dashboard/Consultation/consult-plafond-prime/consult-plafond-prime.component';



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
          { path: 'droits-primes', component: DroitsPrimesComponent },
          { path: 'taux-primes', component: TauxPrimesComponent },
          { path: 'lancer-periode', component: PeriodeComponent },
          { path: 'maj-param', component: MajParamComponent }
        ]
      },
      { 
        path: 'controle', 
        children: [
          { path: 'plafond-prime', component: PlafondPrimeRendementComponent },
          { path: 'indemnite-deplacement', component: IndemniteDeplacementComponent },
          { path: 'avance-prime-resultat', component: AvancePrimeComponent },
          {path:'unite-gestionnaire',component:UniteGestionnaireComponent},
          {path:'controle-pointage',component:ControlePointageComponent},
          { path: 'droits-primes', component: ControlDroitsPrimesComponent },
          { path: 'controle-taux-primes', component: ControleTauxPrimesComponent },
          { path: 'plafond-prime', component: PlafondPrimeRendementComponent }
        ]
      },
      { 
        path: 'consultation', 
        children: [
          { path: 'pointage', component: PointageComponent },
          { path: 'salarie', component: ConsultationSalarieComponent },
         { path: 'consult-plafond-prime', component: ConsultPlafondPrimeComponent }
        ]
      }
      
    ]
  },
  
  
  { path: 'login', canActivate: [loginGuard], component: LoginComponent},

  { path: '**', redirectTo: 'dashboard' }
];