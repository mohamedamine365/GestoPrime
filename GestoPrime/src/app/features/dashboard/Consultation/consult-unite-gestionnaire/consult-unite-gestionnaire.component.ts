import { Component } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consult-unite-gestionnaire',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './consult-unite-gestionnaire.component.html',
  styleUrl: './consult-unite-gestionnaire.component.css'
})
export class ConsultUniteGestionnaireComponent {
items: any[] = []; // Pas d'interface, plus de flexibilité
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  chargerDonnees(): void {
    this.loading = true;
    this.dataService.getConsultationUnitesGestionnaires(this.searchTerm).subscribe({
      next: (data) => {
        this.items = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur de chargement', err);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.chargerDonnees();
  }
}
