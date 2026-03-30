import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../../../core/services/data.service';

@Component({
  selector: 'app-consultation-salarie',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consultation-salarie.component.html',
  styleUrl: './consultation-salarie.component.css'
})
export class ConsultationSalarieComponent implements OnInit {
  // On utilise any[] au lieu du DTO
  salaries: any[] = []; 
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.chargerSalaries();
  }

  chargerSalaries(): void {
    this.loading = true;
    this.dataService.getSalaries(this.searchTerm).subscribe({
      next: (data) => {
        // data est maintenant considéré comme un tableau d'objets génériques
        this.salaries = data; 
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur:', err);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.chargerSalaries();
  }

  initialiser(): void {
    this.searchTerm = '';
    this.chargerSalaries();
  }
}