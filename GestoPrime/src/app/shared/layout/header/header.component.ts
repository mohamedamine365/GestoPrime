import { Component, OnInit } from '@angular/core'; // N'oubliez pas l'import de OnInit
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  user: any = null; // Initialisez à null

  constructor(private authService: AuthenticationService, private router: Router) {}

  ngOnInit(): void {
    const tokenData = this.authService.getDataFromToken();
    if (tokenData) {
      this.user = tokenData;
      // DEBUG: Vérifiez dans la console F12 le nom exact des propriétés
      console.log("Contenu du token :", this.user);
    }
    
  }

  onLogout() {
    localStorage.removeItem('token');
    // Au lieu de reload(), une redirection est plus propre
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }
}