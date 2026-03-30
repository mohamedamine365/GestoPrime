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
  // États des menus
  isAdminMenuOpen: boolean = false;
  isGestionMenuOpen: boolean = false;
  isConsultationMenuOpen: boolean = false;
  isControleMenuOpen: boolean = false; // Ajouté

  user: any = null;

  constructor(private authService: AuthenticationService) {}

  ngOnInit(): void {
    this.user = this.authService.getDataFromToken();
  }

  // --- Gestion des Toggles ---

  toggleAdminMenu() {
    this.isAdminMenuOpen = !this.isAdminMenuOpen;
    if (this.isAdminMenuOpen) {
      this.closeOthers('admin');
    }
  }

  toggleGestionMenu() {
    this.isGestionMenuOpen = !this.isGestionMenuOpen;
    if (this.isGestionMenuOpen) {
      this.closeOthers('gestion');
    }
  }

  toggleConsultationMenu() {
    this.isConsultationMenuOpen = !this.isConsultationMenuOpen;
    if (this.isConsultationMenuOpen) {
      this.closeOthers('consultation');
    }
  }

  toggleControleMenu() { // Ajouté
    this.isControleMenuOpen = !this.isControleMenuOpen;
    if (this.isControleMenuOpen) {
      this.closeOthers('controle');
    }
  }

  /**
   * Ferme les autres menus pour n'en garder qu'un seul ouvert à la fois
   */
  private closeOthers(current: string) {
    if (current !== 'admin') this.isAdminMenuOpen = false;
    if (current !== 'gestion') this.isGestionMenuOpen = false;
    if (current !== 'consultation') this.isConsultationMenuOpen = false;
    if (current !== 'controle') this.isControleMenuOpen = false;
  }
}