import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { Usuario } from '../interfaces/interfaces';
import { HttpClient } from '@angular/common/http';
import { catchError, of, tap, Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = environment.baseUrl;
  private _usuario!: Usuario;

  get usuario() {
    return {...this._usuario};
  }

  constructor(private http: HttpClient) { }

  login(nombreUsuario: string, contrasena: string) {
    const body = {nombreUsuario, contrasena};

    return this.http.post<Usuario>( `${this.baseUrl}/Login`, body )
      .pipe(
        tap(resp => {
          localStorage.setItem('token', resp.token);
          this._usuario = resp
        }),
        catchError(error => of(error))
      );
  }

  validarToken(): Observable<boolean> {
    
    return this.http.get<Usuario>(`${this.baseUrl}/Login/RenovarToken`)
      .pipe(
        map(resp => {
          localStorage.setItem('token', resp.token);
          this._usuario = resp
            return true;
        }),
        catchError( err => of(false))
      );
  }

  logout() {
    localStorage.removeItem('token');
  }

}
