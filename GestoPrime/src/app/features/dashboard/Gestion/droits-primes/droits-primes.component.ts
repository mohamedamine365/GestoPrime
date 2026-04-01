import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DataService } from '../../../../core/services/data.service';
import { ModifierComponent } from './modifier/modifier.component';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-droits-primes',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule],
  templateUrl: './droits-primes.component.html',
  styleUrl: './droits-primes.component.css'
})
export class DroitsPrimesComponent implements OnInit {
  dataSource: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;
  private searchSubject = new Subject<string>();

  // Pagination
  currentPage: number = 1;
  pageSize: number = 10;
  Math = Math; 

  constructor(
    private dataService: DataService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadData();

    // Recherche avec debounce pour ne pas surcharger l'API
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(() => {
      this.loadData();
    });
  }

  loadData(): void {
    this.loading = true;
    // Utilisation du nom exact de votre méthode de service : gettDroitsPrimes
    this.dataService.gettDroitsPrimes(this.searchTerm).subscribe({
      next: (res: any) => {
        // Extraction des données (gestion du format { items: [] } ou [])
        this.dataSource = res.items || res || [];
        this.currentPage = 1;
        this.loading = false;
        console.log(res);
      },
      error: (err) => {
        console.error('Erreur API', err);
        this.loading = false;
        Swal.fire('Erreur', 'Impossible de charger les données.', 'error');
      }
    });
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  openModifierDialog(item: any): void {
    const dialogRef = this.dialog.open(ModifierComponent, {
      width: '550px',
      data: item,
      panelClass: 'custom-dialog-container',
      disableClose: false,
      autoFocus: 'first-tabbable'
    });

    dialogRef.afterClosed().subscribe(result => {
      // Si la modale a appelé updateDroits avec succès et renvoyé true
      if (result === true) {
        this.loadData();
      }
    });
  }

  // --- Logique de Pagination ---
  get paginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.dataSource.slice(start, start + this.pageSize);
  }

  nextPage(): void {
    if (this.currentPage * this.pageSize < this.dataSource.length) {
      this.currentPage++;
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
}