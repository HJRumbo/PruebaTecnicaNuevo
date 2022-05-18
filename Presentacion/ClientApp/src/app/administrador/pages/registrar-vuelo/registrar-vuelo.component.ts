import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Vuelo } from '../../class/vuelo';
import { Ciudad, Aerolinea, Estado } from '../../interfaces/interfaces';
import { AdminService } from '../../services/admin.service';
import { VueloService } from '../../services/vuelo.service';
import { ValidarService } from '../../../shared/services/validar.service';
import Swal from 'sweetalert2';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-registrar-vuelo',
  templateUrl: './registrar-vuelo.component.html',
  styles: [
  ]
})
export class RegistrarVueloComponent implements OnInit {

  minFecha!: string;

  ciudades: Ciudad[] = [];
  aerolineas: Aerolinea[] = [];
  estados: Estado[] = [];

  vuelo: Vuelo = new Vuelo();


  miFormulario: FormGroup = this.fb.group({
    ciudadOrigenId: ['', Validators.required],
    ciudadDestinoId: ['', Validators.required],
    fecha: ['', Validators.required],
    horaSalida: ['', Validators.required],
    horaLlegada: ['', Validators.required],
    aerolineaId: ['', Validators.required],
    estadoVueloId: ['', Validators.required]
  },
  {
    validators: [ this.validarService.camposIguales('ciudadOrigenId', 'ciudadDestinoId'), 
      this.validarService.camposIguales('horaSalida', 'horaLlegada') ]
  })

  constructor(private fb: FormBuilder,
      private router: Router,
      private adminService: AdminService,
      private vueloService: VueloService,
      public validarService: ValidarService) {
        this.validarService.recibirFomulario(this.miFormulario);        
      }

  ngOnInit(): void {
    const date = new Date();
    const datepipe: DatePipe = new DatePipe('en-CO')
    this.minFecha = datepipe.transform(date, 'YYYY-MM-dd')!.toString();

    this.adminService.cosnultarAerolineas()
      .subscribe( a => this.aerolineas = a );

    this.adminService.cosnultarCiudades()
      .subscribe( c => this.ciudades = c );

    this.adminService.cosnultarEstados()
      .subscribe( e => this.estados = e );
  }

  irAConsulta() {
    this.router.navigate(['user/vuelos'])
  }

  guardar() {
    if(this.miFormulario.invalid){
      this.miFormulario.markAllAsTouched();
      return;
    }

    this.vuelo = {...this.miFormulario.value};

    this.vueloService.guardar(this.vuelo)
      .subscribe(resp => {
          if ( resp.ok !== false ) {
            Swal.fire('Vuelo programado correctamente', resp.error, 'success');
            this.miFormulario.reset();
          }else {
            Swal.fire('Error', resp.error, 'error');
          }
        }
      );
    
  }
}
