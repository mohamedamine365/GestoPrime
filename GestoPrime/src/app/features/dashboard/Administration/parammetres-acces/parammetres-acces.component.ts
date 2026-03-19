import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccesService } from '../../../../core/services/acces.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AjoutComponent } from './ajout/ajout.component';
import Swal from 'sweetalert2'; // Import de SweetAlert2

@Component({
  selector: 'app-parammetres-acces',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDialogModule],
  templateUrl: './parammetres-acces.component.html',
  styleUrls: ['./parammetres-acces.component.css']
})
export class ParammetresAccesComponent implements OnInit {
  users: any[] = [];
  searchTerm: string = '';
  isLoading = false;
  
  totalItems = 0;
  currentPage = 1;
  pageSize = 6;
  pageSizeOptions = [5, 10, 25, 50];

  // Configuration globale du Toast SweetAlert
  private Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer)
      toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
  });

  constructor(private accesService: AccesService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.isLoading = true;
    const params = {
      search: this.searchTerm,
      page: this.currentPage,
      pageSize: this.pageSize
    };

    this.accesService.getUsers(params).subscribe({
      next: (res: any) => { 
        this.users = res && res.items ? res.items : (Array.isArray(res) ? res : []);
        this.totalItems = res && res.totalCount ? res.totalCount : this.users.length;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.Toast.fire({ icon: 'error', title: 'Erreur de chargement des données' });
      }
    });
  }

  onSearch() {
    this.currentPage = 1;
    this.loadUsers();
  }

  onDelete(matricule: string) {
    if (!matricule) return;

    // Remplacement du confirm() par SweetAlert2
    Swal.fire({
      title: 'Êtes-vous sûr ?',
      text: `Vous allez révoquer l'accès pour le matricule ${matricule}`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Oui, supprimer !',
      cancelButtonText: 'Annuler'
    }).then((result) => {
      if (result.isConfirmed) {
        this.accesService.deleteUser(matricule).subscribe({
          next: () => {
            this.loadUsers();
            this.Toast.fire({
              icon: 'success',
              title: 'Accès révoqué avec succès'
            });
          },
          error: (err) => {
            console.error('Erreur lors de la suppression :', err);
            this.Toast.fire({
              icon: 'error',
              title: 'Impossible de supprimer cet accès'
            });
          }
        });
      }
    });
  }

  changePage(newPage: number) {
    if (newPage >= 1 && newPage <= this.totalPages) {
      this.currentPage = newPage;
      this.loadUsers();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize) || 1;
  }

  openAjoutModal(): void {
    const dialogRef = this.dialog.open(AjoutComponent, {
      width: '550px',
      data: { matricule: '', nom_Prenom: '', privilege: '', etablissement: '' },
      autoFocus: false
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUsers();
        this.Toast.fire({
          icon: 'success',
          title: 'Nouvel accès créé'
        });
      }
    });
  }
}