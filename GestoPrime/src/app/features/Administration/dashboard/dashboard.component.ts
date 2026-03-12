import { Component } from '@angular/core';
import { SidebarComponent } from "../../../shared/layout/sidebar/sidebar.component";
import { HeaderComponent } from "../../../shared/layout/header/header.component";

import { FooterComponent } from "../../../shared/layout/footer/footer.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [SidebarComponent, HeaderComponent, RouterOutlet, FooterComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
