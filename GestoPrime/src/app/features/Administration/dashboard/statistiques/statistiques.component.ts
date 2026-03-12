import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgApexchartsModule } from "ng-apexcharts";
import { BoardService } from '../../../../core/services/board.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-statistiques',
  standalone: true,
  imports: [CommonModule, NgApexchartsModule],
  templateUrl: './statistiques.component.html',
  styleUrls: ['./statistiques.component.css']
})
export class StatistiquesComponent implements OnInit {
  public chartDay: any = null;
  public chartMonth: any = null;
  public chartSite: any = null;
  public isLoading = true;
  public lastYear: number = new Date().getFullYear() - 1;

  constructor(private boardService: BoardService) {}

  ngOnInit(): void {
    this.fetchStats();
  }

  fetchStats() {
    this.isLoading = true;
    forkJoin({
      day: this.boardService.getStatsByDay('2023-11-28', '2023-12-10'),
      month: this.boardService.getStatsByMonth(), // Doit renvoyer les stats de l'année
      site: this.boardService.getStatsBySite()
    }).subscribe({
      next: (res) => {
        this.initDayChart(res.day);
        this.initMonthChart(res.month);
        this.initSiteChart(res.site);
        this.isLoading = false;
      },
      error: (err) => {
        console.error("Erreur stats:", err);
        this.isLoading = false;
      }
    });
  }

 private initMonthChart(data: any[]) {
  // 1. On définit les labels manuellement pour éviter le "undefined"
  const nomsMois = ['Janv', 'Févr', 'Mars', 'Avril', 'Mai', 'Juin', 'Juil', 'Août', 'Sept', 'Oct', 'Nov', 'Déc'];
  
  // 2. On prépare les données (12 mois)
  let valeursFinales = new Array(12).fill(0);
  
  if (data && data.length > 0) {
    data.forEach((item, index) => {
      // Si l'API renvoie 12 valeurs, on les aligne, sinon on utilise l'index
      if (index < 12) {
        valeursFinales[index] = item.count || item.valeur || 0;
      }
    });
  }

  this.chartMonth = {
    series: [{ name: "Flux", data: valeursFinales }],
    chart: { type: "area", height: 380, toolbar: { show: false } },
    // ... reste de votre config (stroke, fill, etc.)
    xaxis: { 
      categories: nomsMois, // On utilise les labels définis plus haut
      labels: { style: { colors: '#94a3b8' } }
    },
    tooltip: {
      theme: 'light',
      x: { formatter: (val: number, opts: any) => nomsMois[opts.dataPointIndex] }
    }
  };
}

  // ... Autres méthodes initDayChart et initSiteChart identiques
  private initDayChart(data: any[]) {
    const filteredData = data.slice(-6);
    this.chartDay = {
      series: [{ name: "Mouvements", data: filteredData.map(d => d.count || d.valeur) }],
      chart: { type: "bar", height: 350, toolbar: { show: false } },
      plotOptions: { bar: { columnWidth: '50%', borderRadius: 8, distributed: true } },
      xaxis: { categories: filteredData.map(d => d.label || d.date) },
      legend: { show: false },
      colors: ['#4e73df', '#1cc88a', '#36b9cc', '#f6c23e', '#e74a3b', '#858796']
    };
  }

  private initSiteChart(data: any[]) {
    this.chartSite = {
      series: data.map(d => d.count),
      labels: data.map(d => d.label),
      chart: { type: "donut", height: 350 },
      colors: ['#4e73df', '#1cc88a', '#f6c23e', '#e74a3b', '#36b9cc'],
      legend: { position: 'bottom' }
    };
  }
}