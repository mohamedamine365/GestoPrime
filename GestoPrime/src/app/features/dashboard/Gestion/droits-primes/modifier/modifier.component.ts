import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; 
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { DataService } from '../../../../../core/services/data.service';

@Component({
  selector: 'app-modifier',
  standalone: true,
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
    // Initialisation du formulaire : les noms doivent matcher le formControlName du HTML
    this.droitsForm = this.fb.group({
      id: [this.data.id], 
      uniteGestionnaire: [
        { value: this.data.uniteGestionnaire, disabled: true }, 
        Validators.required
      ],
      mat_Resp: [this.data.maT_RESP || this.data.mat_Resp, Validators.required],
      nom_Prenom_Resp: [
        { value: this.data.noM_PRENOM_RESP || this.data.nom_Prenom_Resp, disabled: true }
      ],
      droit_Hygiene: [this.data.droit_Hygiene],
      droit_Prod: [this.data.droit_Prod]
    });
  }

  onSave() {
  if (this.droitsForm.valid) {
    // getRawValue permet de récupérer les champs "disabled" comme l'unité et l'ID
    const rawValues = this.droitsForm.getRawValue();

    const payload = {
      Id: rawValues.id, 
      // On garde la valeur EXACTE reçue (avec les espaces) pour le mapping SQL
      Unite_Gestionnaire: rawValues.uniteGestionnaire, 
      MAT_RESP: rawValues.mat_Resp,
      Droit_Hygiene: rawValues.droit_Hygiene,
      Droit_Prod: rawValues.droit_Prod,
      Utilisateur: 'Admin'
    };

    this.dataService.updateDroits(payload).subscribe({
      next: (res) => {
        Swal.fire('Succès', 'Modifications enregistrées', 'success');
        this.dialogRef.close(true);
      },
      error: (err) => {
        // C'est ici que ton erreur "introuvable" est captée
        Swal.fire('Erreur', err.error?.message || 'Erreur serveur', 'error');
      }
    });
  }
}
  onCancel(): void {
    this.dialogRef.close(false);
  }
}