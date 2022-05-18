import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdministradorRoutingModule } from './administrador-routing.module';
import { ConsultarVuelosComponent } from './pages/consultar-vuelos/consultar-vuelos.component';
import { DetallesVueloComponent } from './pages/detalles-vuelo/detalles-vuelo.component';
import { RegistrarVueloComponent } from './pages/registrar-vuelo/registrar-vuelo.component';
import { HomeComponent } from './pages/home/home.component';
import { MaterialModule } from '../material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReactiveFormsModule } from '@angular/forms';
import { ConvertirFechaPipe } from './pipes/convertir-fecha.pipe';


@NgModule({
  declarations: [
    ConsultarVuelosComponent,
    DetallesVueloComponent,
    RegistrarVueloComponent,
    HomeComponent,
    ConvertirFechaPipe
  ],
  imports: [
    CommonModule,
    AdministradorRoutingModule,
    MaterialModule,
    FlexLayoutModule,
    ReactiveFormsModule
  ]
})
export class AdministradorModule { }
