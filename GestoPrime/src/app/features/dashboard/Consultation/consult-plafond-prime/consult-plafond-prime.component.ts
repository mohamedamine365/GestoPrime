import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consult-plafond-prime',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consult-plafond-prime.component.html',
  styleUrl: './consult-plafond-prime.component.css'
})
export class ConsultPlafondPrimeComponent implements OnInit {
  items: any[] = [];
  searchTerm: string = '';

  constructor(private service: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  chargerDonnees(): void {
    this.service.getConsultationsPlafondPrime(this.searchTerm).subscribe({
      next: (response) => {
        // Mapping des données reçues du SIRH
        this.items = response.items;
      },
      error: (err) => console.error('Erreur lors du chargement des plafonds:', err)
    });
  }
}