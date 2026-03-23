import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { DataService } from '../../../core/services/data.service';
import { ModifierComponent } from './modifier/modifier.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-droits-primes',
  standalone: true,
  templateUrl: './droits-primes.component.html',
  styleUrls: ['./droits-primes.component.css'],
  imports: [CommonModule, FormsModule, MatDialogModule, ModifierComponent],
})
export class DroitsPrimesComponent implements OnInit {
  dataSource: any[] = [];
  searchTerm: string = '';
  
  // Pagination logic
  currentPage: number = 1;
  pageSize: number = 10;
  Math = Math; // Pour utiliser Math.min dans le HTML

  constructor(private dataService: DataService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.dataService.getDroitsPrimes(this.searchTerm).subscribe({
      next: (res) => {
        this.dataSource = res.items || [];
        this.currentPage = 1; // Reset à la page 1 sur recherche
      },
      error: (err) => console.error('Erreur chargement données', err)
    });
  }

  // Getter pour obtenir uniquement les données de la page actuelle
  get paginatedData() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.dataSource.slice(start, start + this.pageSize);
  }

  // Navigation
  nextPage() {
    if (this.currentPage * this.pageSize < this.dataSource.length) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  onSearchChange() {
    this.loadData();
  }

  openModifierDialog(data: any) {
    const dialogRef = this.dialog.open(ModifierComponent, {
      width: '500px',
      data: data,
      panelClass: 'custom-dialog-container',
      disableClose: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) this.loadData();
    });
  }
}