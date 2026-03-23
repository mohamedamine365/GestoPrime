import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Pour les pipes et directives de base
import { DataService } from '../../../../core/services/data.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-modifier',
  standalone: true,
  // Correction des imports : ajout de ReactiveFormsModule et CommonModule
  imports: [ReactiveFormsModule, CommonModule], 
  templateUrl: './modifier.component.html',
  styleUrl: './modifier.component.css'
})
export class ModifierComponent implements OnInit {
  droitsForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
    public dialogRef: MatDialogRef<ModifierComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {
    // Initialisation du formulaire avec les données reçues de la ligne du tableau
    this.droitsForm = this.fb.group({
      // Utilisation des noms de colonnes SQL pour la cohérence
      unite_Gestionnaire: [{ value: this.data.unite_Gestionnaire, disabled: true }, Validators.required],
      mat_Resp: [this.data.maT_RESP || this.data.mat_Resp, Validators.required],
      nom_Prenom_Resp: [{ value: this.data.noM_PRENOM_RESP || this.data.nom_Prenom_Resp, disabled: true }],
      droit_Hygiene: [this.data.droit_Hygiene],
      droit_Prod: [this.data.droit_Prod]
    });

    // Écouteur sur le matricule pour remplissage AUTO (Capture 10)
    this.droitsForm.get('mat_Resp')?.valueChanges.subscribe(val => {
      if (val && val.length >= 8) { 
        this.dataService.lookupByMatricule(val).subscribe({
          next: (res) => {
            if (res) {
              // Mise à jour automatique de l'unité et du nom
              this.droitsForm.patchValue({
                unite_Gestionnaire: res.unite_Gestionnaire,
                nom_Prenom_Resp: res.noM_PRENOM_RESP
              });
            }
          }
        });
      }
    });
  }

onSave() {
  if (this.droitsForm.valid) {
    const payload = this.droitsForm.getRawValue();

    // Confirmation avant action
    Swal.fire({
      title: 'Confirmer la modification ?',
      text: `Vous allez modifier les droits de l'unité : ${payload.unite_Gestionnaire}`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonColor: '#4f46e5', // Couleur violette pro
      cancelButtonColor: '#6e7d88',
      confirmButtonText: 'Oui, modifier',
      cancelButtonText: 'Annuler',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        
        // Affichage du Loader
        Swal.fire({
          title: 'Mise à jour...',
          allowOutsideClick: false,
          didOpen: () => {
            Swal.showLoading();
          }
        });

        this.dataService.updateDroits(payload).subscribe({
          next: (res) => {
            Swal.fire({
              icon: 'success',
              title: 'Mis à jour !',
              text: 'Les modifications ont été enregistrées.',
              timer: 2000,
              showConfirmButton: false
            }).then(() => {
              this.dialogRef.close(true);
            });
          },
          error: (err) => {
            Swal.fire({
              icon: 'error',
              title: 'Erreur',
              text: err.error?.message || 'Impossible de mettre à jour les données.',
            });
          }
        });
      }
    });
  }
}
  

  onCancel(): void {
    this.dialogRef.close(false);
  }
}