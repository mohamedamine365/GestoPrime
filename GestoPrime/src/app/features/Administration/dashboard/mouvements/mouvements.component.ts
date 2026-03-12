import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MouvementService } from '../../../../core/services/mouvement.service';

@Component({
  selector: 'app-mouvements',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mouvements.component.html',
  styleUrls: ['./mouvements.component.css']
})
export class MouvementsComponent implements OnInit, OnDestroy {
  mouvements: any[] = [];
  isLoading = false;
  totalItems = 0;
  
  private searchTimeout: any;

  filters = {
    search: '',
    resultat: '',
    page: 1,
    pageSize: 10
  };

  pageSizeOptions = [10, 25, 50, 100];

  constructor(private mvtService: MouvementService) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    if (this.searchTimeout) clearTimeout(this.searchTimeout);
  }

  /**
   * Appelle l'API .NET avec les filtres actuels
   */
  loadData() {
    this.isLoading = true;
    this.mvtService.getMouvements(this.filters).subscribe({
      next: (res) => {
        // Mapping de la réponse { items: [], totalCount: X }
        this.mouvements = res.items || [];
        this.totalItems = res.totalCount || 0;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Erreur API:', err);
        this.isLoading = false;
      }
    });
  }

  /**
   * Gère le changement de filtres avec un délai de 400ms (Debounce)
   */
  onFilterChange() {
    this.filters.page = 1; 
    if (this.searchTimeout) clearTimeout(this.searchTimeout);

    this.searchTimeout = setTimeout(() => {
      this.loadData();
    }, 400);
  }

  changePage(newPage: number) {
    if (newPage >= 1 && newPage <= this.totalPages) {
      this.filters.page = newPage;
      this.loadData();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.filters.pageSize) || 1;
  }

  // --- Exports (Utilisent les données actuellement chargées dans le tableau) ---

 // --- Méthodes d'exportation complètes ---

downloadExcel() {
  this.isLoading = true;
  // On crée une copie des filtres sans pagination pour avoir TOUT
  const exportFilters = { ...this.filters, page: 1, pageSize: 999999 }; 

  this.mvtService.getMouvements(exportFilters).subscribe({
    next: (res) => {
      this.mvtService.exportToExcel(res.items, 'Journal_Mouvements_Complet');
      this.isLoading = false;
    },
    error: () => this.isLoading = false
  });
}

downloadPdf() {
  this.isLoading = true;
  const exportFilters = { ...this.filters, page: 1, pageSize: 999999 };

  this.mvtService.getMouvements(exportFilters).subscribe({
    next: (res) => {
      this.mvtService.exportToPdf(res.items, 'Journal_Mouvements_Complet');
      this.isLoading = false;
    },
    error: () => this.isLoading = false
  });
}
}