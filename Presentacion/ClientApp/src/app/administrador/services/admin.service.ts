import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Ciudad, Aerolinea, Estado } from '../interfaces/interfaces';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseUrl: string;
  
  constructor(private http: HttpClient) { 
    this.baseUrl = environment.baseUrl;
  }

  cosnultarCiudades(): Observable<Ciudad[]>{
    return this.http.get<Ciudad[]>(`${this.baseUrl}/Ciudad`);
  }

  cosnultarAerolineas():Observable<Aerolinea[]>{
    return this.http.get<Aerolinea[]>(`${this.baseUrl}/Aerolinea`);
  }

  cosnultarEstados():Observable<Estado[]>{
    return this.http.get<Estado[]>(`${this.baseUrl}/Estado`);
  }
}
