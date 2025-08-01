
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormatDatePipe } from 'src/app/shared/pipes/format-date.pipe';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
    selector: 'app-home',
    imports: [RouterOutlet, NavbarComponent, FormatDatePipe],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {
  title: any = 'Frontend';
  date: any = new Date();
}
