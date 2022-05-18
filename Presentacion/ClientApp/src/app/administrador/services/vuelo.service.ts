import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, map, catchError, of } from 'rxjs';
import { Vuelo } from '../class/vuelo';

@Injectable({
  providedIn: 'root'
})
export class VueloService {
  baseUrl: string;
  
  constructor(private http: HttpClient) { 
    this.baseUrl = environment.baseUrl;
  }

  guardar(vuelo: Vuelo) {
    return this.http.post<Vuelo>(`${this.baseUrl}/Vuelo`, vuelo)
      .pipe(
        map(resp => {
          return resp;
        }),
        catchError(error => of(error))
      );
  }

  consultar() {
    return this.http.get<Vuelo[]>(`${this.baseUrl}/Vuelo`)
      .pipe(
        map(resp => {
          return resp;
        }),
        catchError(error => of(error))
      );
  }

  consultarVuelo(id: number) {
    return this.http.get<Vuelo>(`${this.baseUrl}/Vuelo/${id}`)
      .pipe(
        map(resp => {
          return resp;
        }),
        catchError(error => of(error))
      );
  }
}
