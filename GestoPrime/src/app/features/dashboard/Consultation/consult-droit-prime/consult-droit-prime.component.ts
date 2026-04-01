import { Component } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-consult-droit-prime',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './consult-droit-prime.component.html',
  styleUrl: './consult-droit-prime.component.css'
})
export class ConsultDroitPrimeComponent {
items: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.chargerDonnees();
  }

  chargerDonnees(): void {
    this.loading = true;
    this.dataService.getConsultationDroitsPrimes(this.searchTerm).subscribe({
      next: (data) => {
        this.items = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }
}
