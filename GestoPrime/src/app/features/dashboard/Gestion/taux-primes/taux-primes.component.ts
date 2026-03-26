import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../../../core/services/data.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog'; // Ajout de MatDialogModule
import { ModifierComponent } from './modifier/modifier.component';

@Component({
  selector: 'app-taux-primes',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule], // Obligatoire ici
  templateUrl: './taux-primes.component.html',
  styleUrls: ['./taux-primes.component.css']
})
export class TauxPrimesComponent implements OnInit {
  dataSource: any[] = [];
  searchTerm: string = '';

  constructor(private dataService: DataService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.dataService.getTauxPrimes(this.searchTerm).subscribe({
      next: (res) => this.dataSource = res.items || [],
      error: (err) => console.error('Erreur API Get:', err)
    });
  }

  
  openModifier(item: any): void {
  const dialogRef = this.dialog.open(ModifierComponent, {
    width: '500px',
    data: { ...item }, // On envoie toujours une copie
    panelClass: 'saas-dialog-overlay'
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result === true) {
      // Si result est true, c'est que l'API a réussi dans le ModifierComponent
      this.loadData(); 
      // Optionnel : Afficher un toast de succès ici
    }
  });
}
}