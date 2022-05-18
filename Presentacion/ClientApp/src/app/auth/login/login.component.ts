import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidarService } from '../../shared/services/validar.service';
import { Router } from '@angular/router';

import Swal from "sweetalert2";

import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
  ]
})
export class LoginComponent {

  miFormulario: FormGroup = this.fb.group({
    nombreUsuario: ['', Validators.required],
    contrasena: ['', Validators.required]
  })

  constructor(private fb: FormBuilder, 
    public validarService: ValidarService,
    private authService: AuthService,
    private router: Router) {
    this.validarService.recibirFomulario(this.miFormulario);
  }

  login() {
    if(this.miFormulario.invalid){
      this.miFormulario.markAllAsTouched();
      return;
    }

    const { nombreUsuario, contrasena} = this.miFormulario.value;
    
    this.authService.login(nombreUsuario, contrasena)
      .subscribe(resp => {
        if ( resp.ok !== false ) {
          this.router.navigate(['/user/vuelos'])
        }else {
          Swal.fire('Error', resp.error, 'error');
        }
      })
  }

}
