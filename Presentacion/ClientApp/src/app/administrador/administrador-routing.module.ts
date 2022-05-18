import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ConsultarVuelosComponent } from './pages/consultar-vuelos/consultar-vuelos.component';
import { DetallesVueloComponent } from './pages/detalles-vuelo/detalles-vuelo.component';
import { RegistrarVueloComponent } from './pages/registrar-vuelo/registrar-vuelo.component';
import { ValidarTokenGuard } from '../auth/guards/validar-token.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: 'vuelos',
        component: ConsultarVuelosComponent
      },
      {
        path: 'detalles-vuelo/:id',
        component: DetallesVueloComponent
      },
      {
        path: 'registrar-vuelo',
        component: RegistrarVueloComponent,
        canActivate: [ ValidarTokenGuard ],
        canLoad: [ ValidarTokenGuard ],
        data: {
          role: 'ADMIN'
        }
      },
      {
        path: '**',
        redirectTo: 'vuelos'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradorRoutingModule { }
