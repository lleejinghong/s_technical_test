import { Component } from '@angular/core';

import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FormatDatePipe } from './shared/pipes/format-date.pipe';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, NavbarComponent, FormatDatePipe]
})
export class AppComponent {
  title: any = 'Frontend';
  date: any = new Date();
}
