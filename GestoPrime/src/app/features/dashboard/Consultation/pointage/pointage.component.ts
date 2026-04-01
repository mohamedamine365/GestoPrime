import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { DataService } from '../../../../core/services/data.service'; // Ajuste le chemin selon ton projet

@Component({
  selector: 'app-pointage',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pointage.component.html',
  styleUrl: './pointage.component.css'
})
export class PointageComponent implements OnInit, OnDestroy {
  // Liste des données (V_CONSULTATION_POINTAGE)
  dataSource: any[] = []; 
  
  // Terme de recherche (Unité ou Matricule)
  searchTerm: string = '';
  
  // État du spinner
  loading: boolean = false;

  // Gestion de la recherche optimisée
  private searchSubject = new Subject<string>();
  private searchSubscription?: Subscription;

  constructor(private consultationService: DataService) {}

  ngOnInit(): void {
    // 1. Setup de la recherche : attend 300ms après la frappe avant d'appeler l'API
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(() => {
      this.loadData();
    });

    // 2. Chargement initial des données
    this.loadData();
  }

  /**
   * Méthode déclenchée par (ngModelChange) dans le HTML
   */
  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  /**
   * Appel du backend pour récupérer les pointages
   */
  loadData() {
  this.loading = true;
  this.consultationService.getPointages(this.searchTerm).subscribe({
    next: (data) => {
      this.dataSource = data;
      this.loading = false;
    },
    error: () => {
      this.loading = false;
      this.dataSource = [];
    }
  });
}

  /**
   * Libération de la mémoire
   */
  ngOnDestroy(): void {
    if (this.searchSubscription) {
      this.searchSubscription.unsubscribe();
    }
  }
}