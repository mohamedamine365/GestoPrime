import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-maj-salarie',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './maj-salarie.component.html',
  styleUrl: './maj-salarie.component.css'
})


export class MajSalarieComponent {
  selectedFile: File | null = null;
  loading: boolean = false;
  salariesImportes: any[] = [];
  errorMessage: string = '';

  constructor(private dataService: DataService) {}

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.errorMessage = '';
      this.salariesImportes = [];
    }
  }

  onUpload(): void {
    if (!this.selectedFile) return;

    this.loading = true;
    this.errorMessage = '';
    
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.dataService.importSalaries(formData).subscribe({
      next: (data: any) => {
        this.salariesImportes = data;
        this.loading = false;
        this.selectedFile = null;
        console.log(data);
      },
      error: (error: HttpErrorResponse) => {
        this.loading = false;
        // Gestion de l'erreur "Unexpected token T" (si le serveur renvoie du texte au lieu de JSON)
        if (typeof error.error === 'string') {
          this.errorMessage = error.error; 
        } else if (error.status === 500) {
          this.errorMessage = "Erreur interne du serveur (500). Vérifiez les doublons dans le fichier Excel.";
        } else {
          this.errorMessage = error.message;
        }
        console.error('Détails de l\'erreur:', error);
      }
    });
  }
}