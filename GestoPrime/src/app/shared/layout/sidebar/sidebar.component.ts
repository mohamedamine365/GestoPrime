import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
 isAdminMenuOpen: boolean = false; 
  user: any = null;

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    this.user = this.authService.getDataFromToken();
    // Votre token contient actuellement le matricule "14762473" dans le champ name
  }

  toggleAdminMenu() {
    this.isAdminMenuOpen = !this.isAdminMenuOpen;
  }
  // À ajouter dans votre classe
isGestionMenuOpen: boolean = false;

toggleGestionMenu() {
  this.isGestionMenuOpen = !this.isGestionMenuOpen;
  
  // Optionnel : Fermer l'administration si la gestion est ouverte
  if (this.isGestionMenuOpen) {
    this.isAdminMenuOpen = false;
  }
}
}