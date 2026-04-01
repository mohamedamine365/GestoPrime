import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-droits-primes',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './droits-primes.component.html',
  styleUrl: './droits-primes.component.css'
})
export class ControlDroitsPrimesComponent implements OnInit {
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

    // Recherche réactive (attend 400ms avant d'appeler l'API)
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(() => {
      this.loadData();
    });
  }

  loadData(): void {
    this.loading = true;
    this.dataService.getControldroitsprime(this.searchTerm).subscribe({
      next: (res: any) => {
        // Adaptation selon si l'API renvoie { items: [] } ou directement []
        this.dataSource = res.items || res || [];
        this.currentPage = 1;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur de chargement', err);
        this.loading = false;
        Swal.fire('Erreur', 'Impossible de charger la liste de contrôle.', 'error');
      }
    });
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  // --- Getters Pagination ---
  get paginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.dataSource.slice(start, start + this.pageSize);
  }

  get totalPages() {
    return Math.ceil(this.dataSource.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }
}