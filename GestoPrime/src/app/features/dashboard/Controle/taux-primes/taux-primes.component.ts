import { Component, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';
import { DataService } from '../../../../core/services/data.service';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-taux-primes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './taux-primes.component.html',
  styleUrl: './taux-primes.component.css'
})
export class ControleTauxPrimesComponent implements OnInit {
  dataSource: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;
  private searchSubject = new Subject<string>();

  // Pagination
  currentPage: number = 1;
  pageSize: number = 10;
  Math = Math;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.loadData();

    // Recherche réactive optimisée
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(() => {
      this.loadData();
    });
  }

  loadData(): void {
    this.loading = true;
    this.dataService.getControleTauxPrimes(this.searchTerm).subscribe({
      next: (res: any) => {
        // Extraction des données du DTO (res.items envoyé par le contrôleur C#)
        this.dataSource = res.items || [];
        this.currentPage = 1;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur API', err);
        this.loading = false;
        Swal.fire('Erreur', 'Impossible de charger les taux de primes.', 'error');
      }
    });
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  // --- Pagination ---
  get paginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.dataSource.slice(start, start + this.pageSize);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= Math.ceil(this.dataSource.length / this.pageSize)) {
      this.currentPage = page;
    }
  }
}
