import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Vuelo } from '../../class/vuelo';
import { VueloService } from '../../services/vuelo.service';

@Component({
  selector: 'app-detalles-vuelo',
  templateUrl: './detalles-vuelo.component.html',
  styles: [
  ]
})
export class DetallesVueloComponent implements OnInit {

  id: number = 0;
  vuelo: Vuelo = new Vuelo();

  constructor(private rutaActiva: ActivatedRoute,
      private router: Router,
      private vueloService: VueloService) { }

  ngOnInit(): void {
    this.consultarVuelo();
  }

  consultarVuelo() {
    this.id = this.rutaActiva.snapshot.params["id"];

    this.vueloService.consultarVuelo(this.id)
      .subscribe(resp => {
        if ( resp.ok !== false ) {
          this.vuelo = resp;
        }else {
          Swal.fire('No se encontró el vuelo. ', resp.error, 'error');
          this.router.navigate(['user/vuelos'])
        }
      })
  }

  irAConsulta() {
    this.router.navigate(['user/vuelos'])
  }

}
