import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataService } from '../../../../core/services/data.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-maj-param',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './maj-param.component.html',
  styleUrl: './maj-param.component.css'
})
export class MajParamComponent {
  loading: boolean = false;

  constructor(private dataService: DataService) {}

  onMiseAJour(): void {
    this.loading = true;

    this.dataService.executerMajParametres().subscribe({
      next: (response) => {
        this.loading = false;
        Swal.fire({
          title: 'Succès !',
          text: response.message || 'Mise à jour terminée avec succès.',
          icon: 'success',
          confirmButtonColor: '#212529'
        });
      },
      error: (err) => {
        this.loading = false;
        Swal.fire({
          title: 'Erreur',
          text: err.error?.error || 'Une erreur est survenue lors du traitement.',
          icon: 'error',
          confirmButtonColor: '#212529'
        });
      }
    });
  }
}