import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AccesService } from '../../../../../core/services/acces.service';
import Swal from 'sweetalert2'; // Import de SweetAlert2

@Component({
  selector: 'app-ajout',
  standalone: true,
  imports: [
    CommonModule, FormsModule, MatDialogModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './ajout.component.html',
  styleUrls: ['./ajout.component.css']
})
export class AjoutComponent {
  isSearching = false;
  isLoading = false;

  constructor(
    public dialogRef: MatDialogRef<AjoutComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private accesService: AccesService
  ) {
    if (!this.data) {
      this.data = { matricule: '', nom_Prenom: '', privilege: 'Gestion', etablissement: '' };
    }
  }

  onMatriculeChange() {
    const mat = this.data.matricule;
    if (mat && mat.length >= 3) {
      this.isSearching = true;
      this.accesService.checkSalarie(mat).subscribe({
        next: (res: any) => {
          this.data.nom_Prenom = res.nomPrenom; 
          this.data.etablissement = res.etablissement;
          this.isSearching = false;
        },
        error: () => {
          this.isSearching = false;
          this.data.nom_Prenom = '';
          this.data.etablissement = '';
        }
      });
    }
  }

  enregistrer() {
    if (this.data.matricule && this.data.nom_Prenom) {
      
      // Optionnel : Ajouter une confirmation SweetAlert avant d'enregistrer
      Swal.fire({
        title: 'Confirmer l\'ajout ?',
        text: `Voulez-vous accorder un accès ${this.data.privilege} à ${this.data.nom_Prenom} ?`,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#aaa',
        confirmButtonText: 'Oui, enregistrer',
        cancelButtonText: 'Annuler'
      }).then((result) => {
        if (result.isConfirmed) {
          this.executeCreation();
        }
      });
    }
  }

  private executeCreation() {
    this.isLoading = true;

    const payload = {
      matricule: String(this.data.matricule),
      nomPrenom: this.data.nom_Prenom,
      etablissement: this.data.etablissement || "N/A",
      privilege: this.data.privilege || "Gestion",
      utilisateur: "automatique",
      date_Mvt: new Date().toISOString()
    };

    this.accesService.createAccess(payload).subscribe({
      next: () => {
        this.isLoading = false;
        
        // Alerte de succès SweetAlert
        Swal.fire({
          icon: 'success',
          title: 'Succès !',
          text: 'L\'accès a été créé avec succès.',
          timer: 2000,
          showConfirmButton: false
        });

        this.dialogRef.close(true);
      },
      error: (err) => {
        this.isLoading = false;
        console.error("Erreur serveur :", err);
        
        // Alerte d'erreur SweetAlert
        Swal.fire({
          icon: 'error',
          title: 'Erreur',
          text: 'Problème de base de données. L\'accès n\'a pas pu être créé.',
          confirmButtonColor: '#d33'
        });
      }
    });
  }

  annuler(): void {
    this.dialogRef.close();
  }
}