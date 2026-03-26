import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pointage',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './pointage.component.html',
  styleUrl: './pointage.component.css'
})
export class PointageComponent implements OnInit {
  // Initialisation obligatoire avec un tableau vide
  dataSource: any[] = []; 
  searchTerm: string = '';
  loading: boolean = false;

  constructor(private consultationService: DataService) {}

  ngOnInit(): void {
    this.loadData();
    
  }

  loadData() {
  console.log("Appel API avec :", this.searchTerm); // Pour voir si la fonction se lance
  
  this.consultationService.getPointages(this.searchTerm).subscribe({
    next: (data) => {
      console.log("Données reçues :", data); // Doit être un tableau []
      this.dataSource = data;
    },
    error: (err) => {
      console.error("Le lien avec le serveur est mort :", err);
    }
  });
}
}