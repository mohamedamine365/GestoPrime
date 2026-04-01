import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { DataService } from '../../../../core/services/data.service';

@Component({
  selector: 'app-avance-prime',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './avance-prime.component.html',
  styleUrl: './avance-prime.component.css'
})
export class AvancePrimeComponent implements OnInit, OnDestroy {
  dataSource: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;

  private searchSubject = new Subject<string>();
  private searchSubscription?: Subscription;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    // La recherche se lance automatiquement quand l'utilisateur tape
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(350),
      distinctUntilChanged()
    ).subscribe(() => this.loadAvances());

    this.loadAvances();
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  loadAvances(): void {
    this.loading = true;
    this.dataService.getAvancesPrime(this.searchTerm).subscribe({
      next: (data) => {
        this.dataSource = data;
        this.loading = false;
      },
      error: () => {
        this.dataSource = [];
        this.loading = false;
      }
    });
  }

  ngOnDestroy(): void {
    this.searchSubscription?.unsubscribe();
  }
}