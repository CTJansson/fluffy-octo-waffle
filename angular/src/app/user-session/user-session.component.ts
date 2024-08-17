import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticationService, Session } from '../authentication.service';
import { CommonModule, NgFor } from '@angular/common';

@Component({
  selector: 'app-user-session',
  standalone: true,
  imports: [
    CommonModule,
    NgFor
  ],
  templateUrl: './user-session.component.html',
  styleUrl: './user-session.component.scss'
})
export class UserSessionComponent {
  public session$: Observable<Session>;
  public isAuthenticated$: Observable<boolean>;
  public isAnonymous$: Observable<boolean>;

  constructor(auth: AuthenticationService) {
    this.session$ = auth.getSession();
    this.isAuthenticated$ = auth.getIsAuthenticated();
    this.isAnonymous$ = auth.getIsAnonymous();
  }
}
