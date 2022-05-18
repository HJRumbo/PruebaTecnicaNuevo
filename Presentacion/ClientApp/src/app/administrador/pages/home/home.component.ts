import { Component } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: [
  ]
})
export class HomeComponent {

  get usuario() {
    return this.authService.usuario;
  }

  constructor(private authService: AuthService,
    private router: Router) { }

  logout() {
    this.router.navigateByUrl('/auth/login');
    this.authService.logout();
  }

}
