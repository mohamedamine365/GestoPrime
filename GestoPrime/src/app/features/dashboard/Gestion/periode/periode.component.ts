import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import Swal from 'sweetalert2';
import { DataService } from '../../../../core/services/data.service';

@Component({
  selector: 'app-periode',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './periode.component.html',
  styleUrl: './periode.component.css'
})
export class PeriodeComponent implements OnInit {
  // Sélection par défaut (Mois actuel)
  selectedAnnee: number = new Date().getFullYear();
  selectedMois: string = ("0" + (new Date().getMonth() + 1)).slice(-2);
  
  loading: boolean = false;
  periodes: any[] = [];

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
      next: (data) => this.periodes = data,
      error: (err) => console.error('Erreur chargement:', err)
    });
  }

  onLancerPeriode() {
    const code = `MT${this.selectedAnnee}${this.selectedMois}`;

    Swal.fire({
      title: 'Confirmer le lancement ?',
      text: `La période ${code} sera créée et la précédente sera clôturée.`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Confirmer',
      cancelButtonText: 'Annuler'
    }).then((result) => {
      if (result.isConfirmed) {
        this.executeLaunch(code);
      }
    });
  }

  private executeLaunch(periodeCode: string) {
    this.loading = true;
    this.dataService.lancerNouvellePeriode(periodeCode).subscribe({
      next: (res) => {
        this.loading = false;
        Swal.fire('Lancé !', res.message, 'success');
        this.chargerHistorique();
      },
      error: (err) => {
        this.loading = false;
        // Affiche l'erreur renvoyée par ton Backend (Regex ou doublon)
        const msg = err.error?.message || 'Erreur lors du lancement';
        Swal.fire('Erreur', msg, 'error');
      }
    });
  }
}