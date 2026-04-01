import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { DataService } from '../../../../core/services/data.service';

@Component({
  selector: 'app-pointage',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './controle-pointage.component.html',
  styleUrl: './controle-pointage.component.css'
})
export class ControlePointageComponent implements OnInit, OnDestroy {
  dataSource: any[] = [];
  searchTerm: string = '';
  loading: boolean = false;

  private searchSubject = new Subject<string>();
  private searchSubscription?: Subscription;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.searchSubscription = this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(() => this.loadData());

    this.loadData();
  }

  onSearchChange(): void {
    this.searchSubject.next(this.searchTerm);
  }

  loadData(): void {
    this.loading = true;
    this.dataService.getControlePointage(this.searchTerm).subscribe({
      next: (data) => {
        this.dataSource = data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.dataSource = [];
        this.loading = false;
      }
    });
  }

  ngOnDestroy(): void {
    this.searchSubscription?.unsubscribe();
  }
}