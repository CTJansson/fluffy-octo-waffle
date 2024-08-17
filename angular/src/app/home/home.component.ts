import { Component } from '@angular/core';
import { TodosComponent } from '../todos/todos.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    TodosComponent
  ],
  templateUrl: './home.component.html',
})
export class HomeComponent {
}
