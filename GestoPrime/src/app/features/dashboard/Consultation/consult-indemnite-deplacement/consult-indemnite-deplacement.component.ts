import { Component } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consult-indemnite-deplacement',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './consult-indemnite-deplacement.component.html',
  styleUrl: './consult-indemnite-deplacement.component.css'
})
export class ConsultIndemniteDeplacementComponent {
items: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  /**
   * Appelle le service pour charger les données de la vue SQL
   */
  chargerDonnees(): void {
    this.loading = true;
    this.dataService.getConsultationPlafondIndemnite(this.searchTerm).subscribe({
      next: (data) => {
        this.items = data;
        this.loading = false;
        console.log(data);
      },
      error: (err) => {
        console.error('Erreur de chargement:', err);
        this.loading = false;
      }
    });
  }

  /**
   * Déclenché à chaque modification du champ de recherche
   */
  onSearch(): void {
    this.chargerDonnees();
  }
}
