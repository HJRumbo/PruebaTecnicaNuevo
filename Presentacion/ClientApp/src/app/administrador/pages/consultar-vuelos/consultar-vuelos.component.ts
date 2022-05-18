import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VueloService } from '../../services/vuelo.service';
import { Vuelo } from '../../class/vuelo';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-consultar-vuelos',
  templateUrl: './consultar-vuelos.component.html',
  styles: [
    `
      table {
        width: 100%;
      }
    `
  ]
})
export class ConsultarVuelosComponent implements OnInit {

  vuelos: Vuelo[] = [];

  displayedColumns: string[] = ['numeroVuelo', 'ciudadOrigen', 'ciudadDestino', 'fecha', 'detalles'];

  get usuario() {
    return this.authService.usuario;
  }

  constructor(private router: Router,
    private vueloService: VueloService,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.vueloService.consultar()
      .subscribe(v => this.vuelos = v);
  }

  irARegistro() {
    this.router.navigate(['user/registrar-vuelo']);
  }

  ver(numeroVuelo: number) {
    this.router.navigateByUrl(`user/detalles-vuelo/${numeroVuelo}`)
  }

}
