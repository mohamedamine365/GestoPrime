import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consult-prime-avance',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './consult-prime-avance.component.html',
  styleUrl: './consult-prime-avance.component.css'
})
export class ConsultAvanceComponent implements OnInit {
  items: any[] = []; // On utilise any pour plus de souplesse
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  chargerDonnees(): void {
    this.loading = true;
    this.dataService.getConsultationAvancePrime(this.searchTerm).subscribe({
      next: (data) => {
        this.items = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur:', err);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.chargerDonnees();
  }
}