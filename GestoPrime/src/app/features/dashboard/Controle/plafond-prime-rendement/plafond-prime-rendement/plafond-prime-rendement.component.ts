import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-plafond-prime-rendement',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './plafond-prime-rendement.component.html',
  styleUrl: './plafond-prime-rendement.component.css'
})
export class PlafondPrimeRendementComponent implements OnInit {
  dataList: any[] = [];
  loading: boolean = false;
  rechercheGlobale: string = '';

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  /**
   * Charge les données en utilisant le filtre global
   */
  chargerDonnees(): void {
    this.loading = true;

    // On récupère le texte sans espaces inutiles
    const terme = this.rechercheGlobale?.trim();

    // CORRECTION : On appelle getAll avec UN SEUL argument 
    // car le Service et le Backend attendent désormais 'recherche'
    this.dataService.getAllPlafonds(terme).subscribe({
      next: (data: any[]) => {
        // Le Service s'occupe déjà d'extraire le tableau, 
        // on peut donc assigner directement.
        this.dataList = data;
        this.loading = false;
        
        console.log('Total éléments affichés :', this.dataList.length);
      },
      error: (err) => {
        console.error('Erreur lors du chargement des données', err);
        this.dataList = [];
        this.loading = false;
      }
    });
  }

  /**
   * Réinitialise la barre de recherche et recharge la liste complète
   */
  initialiser(): void {
    this.rechercheGlobale = '';
    this.chargerDonnees();
  }
}