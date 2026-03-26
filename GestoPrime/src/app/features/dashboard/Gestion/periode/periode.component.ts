import { Component, OnInit } from '@angular/core';
import { DataService } from '../../../../core/services/data.service';
import Swal from 'sweetalert2';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-periode',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './periode.component.html',
  styleUrl: './periode.component.css'
})
export class PeriodeComponent implements OnInit {
  selectedAnnee: number = new Date().getFullYear();
  selectedMois: string = ("0" + (new Date().getMonth() + 1)).slice(-2);
  loading: boolean = false;
  periodes: any[] = []; // Stockage de l'historique

  annees: number[] = [2024, 2025, 2026, 2027];
  moisList = [
    { v: '01', n: 'Janvier' }, { v: '02', n: 'Février' }, { v: '03', n: 'Mars' },
    { v: '04', n: 'Avril' }, { v: '05', n: 'Mai' }, { v: '06', n: 'Juin' },
    { v: '07', n: 'Juillet' }, { v: '08', n: 'Août' }, { v: '09', n: 'Septembre' },
    { v: '10', n: 'Octobre' }, { v: '11', n: 'Novembre' }, { v: '12', n: 'Décembre' }
  ];

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.chargerHistorique();
  }

  chargerHistorique() {
    this.dataService.getPeriodes().subscribe({
      next: (data) => {
        this.periodes = data;
      },
      error: (err) => console.error('Erreur historique:', err)
    });
  }

  onLancerPeriode() {
    const periodeCode = `MT${this.selectedAnnee}${this.selectedMois}`;

    Swal.fire({
      title: 'Confirmation',
      text: `Voulez-vous vraiment lancer la période ${periodeCode} ? Cela clôturera la période précédente.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Oui, lancer !',
      cancelButtonText: 'Annuler'
    }).then((result) => {
      if (result.isConfirmed) {
        this.executeLaunch(periodeCode);
      }
    });
  }

  private executeLaunch(code: string) {
    this.loading = true;
    this.dataService.lancerNouvellePeriode(code).subscribe({
      next: (res) => {
        this.loading = false;
        Swal.fire('Succès', 'La période a été lancée.', 'success');
        this.chargerHistorique(); // Rafraîchir la liste après le lancement
      },
      error: (err) => {
        this.loading = false;
        const errorMsg = err.error?.error || 'Erreur lors du lancement';
        Swal.fire('Erreur', errorMsg, 'error');
      }
    });
  }
}