import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';
import { formatDate } from '@angular/common'; // Import Angular pour les dates

@Injectable({
  providedIn: 'root'
})
export class MouvementService {
  private apiMvtUrl = 'https://localhost:7079/api/Mouvement';

  constructor(private http: HttpClient) {}

  getMouvements(filters: any = {}): Observable<any> {
    let params = new HttpParams();
    Object.keys(filters).forEach(key => {
      const value = filters[key];
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, value.toString());
      }
    });
    return this.http.get<any>(this.apiMvtUrl, { params });
  }

  exportToExcel(data: any[], fileName: string) {
    const dataToExport = data.map(m => ({
      'Matricule': m.matricule,
      'Collaborateur': (m.nomPrenom || m.nom_Prenom || '---').toUpperCase(),
      'Date Mouvement': m.date_Mvt ? formatDate(m.date_Mvt, 'dd/MM/yyyy HH:mm', 'en-US') : '---',
      'Site': m.site || 'N/A',
      'Résultat': m.resultat === 'Success' ? 'Réussi' : 'Échec'
    }));

    const worksheet = XLSX.utils.json_to_sheet(dataToExport);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Journal');
    worksheet['!cols'] = [{wch:12}, {wch:35}, {wch:20}, {wch:20}, {wch:12}];

    XLSX.writeFile(workbook, `${fileName}_${formatDate(new Date(), 'yyyyMMdd_HHmm', 'en-US')}.xlsx`);
  }

  exportToPdf(data: any[], fileName: string) {
    const doc = new jsPDF();
    
    doc.setFontSize(18);
    doc.setTextColor(78, 115, 223); 
    doc.text('Journal des Mouvements SIRH', 14, 20);
    
    doc.setFontSize(9);
    doc.setTextColor(100);
    doc.text(`Généré le : ${formatDate(new Date(), 'dd/MM/yyyy HH:mm', 'en-US')}`, 14, 28);

    const columns = ["Matricule", "Collaborateur", "Date & Heure", "Site", "Résultat"];
    const rows = data.map(m => [
      m.matricule, 
      (m.nomPrenom || m.nom_Prenom || '---').toUpperCase(),
      m.date_Mvt ? formatDate(m.date_Mvt, 'dd/MM/yyyy HH:mm', 'en-US') : '---', 
      m.site || '---',
      m.resultat === 'Success' ? 'Réussi' : 'Échec'
    ]);

    autoTable(doc, {
      startY: 35,
      head: [columns],
      body: rows,
      theme: 'striped',
      headStyles: { fillColor: [78, 115, 223], halign: 'center' },
      styles: { fontSize: 8 },
      // Petite touche "Smart" : Colorer le texte du résultat
      didParseCell: (data) => {
        if (data.section === 'body' && data.column.index === 4) {
          const val = data.cell.raw;
          if (val === 'Échec') {
            data.cell.styles.textColor = [220, 53, 69]; // Rouge Bootstrap
            data.cell.styles.fontStyle = 'bold';
          } else {
            data.cell.styles.textColor = [40, 167, 69]; // Vert Bootstrap
          }
        }
      }
    });
    
    doc.save(`${fileName}.pdf`);
  }
}