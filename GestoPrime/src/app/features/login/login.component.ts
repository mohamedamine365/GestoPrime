import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UserService } from '../../core/services/user.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule,MatSnackBarModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  // Objet lié au formulaire via ngModel
  user = {
    username: '',
    password: ''
  };

  isLoading = false;

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  onLogin() {
    this.isLoading = true;

    this.userService.signin(this.user).subscribe({
      next: (res: any) => {
        this.isLoading = false;
        // On stocke le token (vérifie si ton API renvoie 'token' ou 'mytoken')
        localStorage.setItem('token', res.token || res.mytoken);
        
        this.snackBar.open('Connexion réussie !', 'OK', { duration: 2000 });
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.isLoading = false;
        const errorMsg = err.error?.message || 'Identifiants invalides';
        this.snackBar.open(errorMsg, 'Fermer', { 
          duration: 4000,
          panelClass: ['error-snackbar'] 
        });
      }
    });
  }
}
