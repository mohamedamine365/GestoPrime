import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DataService } from '../../../../../core/services/data.service';


@Component({
  selector: 'app-modifier',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modifier.component.html',
  styleUrl: './modifier.component.css'
})
export class ModifierComponent {
  loading = false;
  errorMessage = '';

  constructor(
    public dialogRef: MatDialogRef<ModifierComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dataService: DataService // Ajout indispensable du service
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (!this.data.id) {
      this.errorMessage = "ID manquant pour la mise à jour.";
      return;
    }

    this.loading = true;
    console.log("Données envoyées :", this.data); 

    // Appel au service avec gestion de l'abonnement (subscribe)
    this.dataService.updateTauxPrime(this.data.id, this.data).subscribe({
      next: (response) => {
        this.loading = false;
        console.log("Mise à jour réussie :", response);
        this.dialogRef.close(true); // On renvoie 'true' pour rafraîchir la liste parente
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = "Erreur lors de la modification.";
        console.error("Erreur serveur :", err);
      }
    });
  }
}