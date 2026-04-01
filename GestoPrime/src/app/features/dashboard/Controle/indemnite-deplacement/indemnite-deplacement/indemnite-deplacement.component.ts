import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../../../../core/services/data.service';

@Component({
  selector: 'app-indemnite-deplacement',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './indemnite-deplacement.component.html',
  styleUrl: './indemnite-deplacement.component.css'
})
export class IndemniteDeplacementComponent implements OnInit {
  dataList: any[] = [];
  loading: boolean = false;
  recherche: string = '';

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  chargerDonnees(): void {
    this.loading = true;
    const terme = this.recherche?.trim();

    this.dataService.getIndemnitesDeplacement(terme).subscribe({
      next: (data) => {
        this.dataList = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur:', err);
        this.dataList = [];
        this.loading = false;
      }
    });
  }

  initialiser(): void {
    this.recherche = '';
    this.chargerDonnees();
  }
}