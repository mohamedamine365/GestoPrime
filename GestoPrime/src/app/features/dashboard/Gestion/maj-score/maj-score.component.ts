import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-maj-score',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './maj-score.component.html',
  styleUrl: './maj-score.component.css'
})
export class MajScoreComponent {
selectedFile: File | null = null;
  loading: boolean = false;
  errorMessage: string = '';
  scoresImportes: any[] = [];

  constructor(private dataService: DataService) {}

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      this.errorMessage = '';
      this.scoresImportes = [];
    }
  }

  onUpload(): void {
    if (!this.selectedFile) return;

    this.loading = true;
    this.errorMessage = '';
    
    const formData = new FormData();
    formData.append('file', this.selectedFile);

    // Note: Assurez-vous d'avoir ajouté cette méthode dans votre DataService
    this.dataService.importScore(formData).subscribe({
      next: (data: any) => {
        this.scoresImportes = data;
        this.loading = false;
        this.selectedFile = null;
      },
    error: (error: HttpErrorResponse) => {
  this.loading = false;
  // Ceci va extraire le message précis du crash serveur si disponible
  if (error.error && error.error.message) {
    this.errorMessage = "Détail : " + error.error.message;
  } else {
    this.errorMessage = "Le serveur a rencontré une erreur (500). Vérifiez le format des colonnes Excel.";
  }
  console.log("Objet d'erreur complet :", error);
}
    });
  }
}
