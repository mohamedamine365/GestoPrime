import { Component } from '@angular/core';

import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "../../shared/layout/footer/footer.component";
import { SidebarComponent } from "../../shared/layout/sidebar/sidebar.component";
import { HeaderComponent } from "../../shared/layout/header/header.component";


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterOutlet, FooterComponent, SidebarComponent, HeaderComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
