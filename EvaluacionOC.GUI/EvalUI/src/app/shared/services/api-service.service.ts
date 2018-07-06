import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Login } from '../model/login';
import { Observable } from 'rxjs';
import { User } from '../model/user';
import { Token } from '../model/token';
import { UsuarioModificar } from '../model/usuario-modificar';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { UsuarioPassword } from '../model/usuario-password';
@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router, private location: Location) {
  }
  private token: Token = {
    token: ''
  };
  private apiUrl = 'api/user';

  login(model: Login): Observable<any> {
    const body = JSON.stringify(model);
    const params = body;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const url = this.baseUrl + this.apiUrl + '/login';
    return this.http.post<any>(url, params, { headers: headers });
    // return null;
  }
  cambiarPassword(model: UsuarioPassword) {
    if (!this.obtenerToken()) {
      return;
    }
    const body = JSON.stringify(model);
    const params = body;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.token.token
    });
    const url = this.baseUrl + this.apiUrl + '/CambioPassword';
    return this.http.put(url, params, { headers: headers });
  }
  verificaLogin() {
    if (!this.obtenerToken()) {
      return;
    }
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.token.token
    });
    const url = this.baseUrl + this.apiUrl + '/VerificaLoged';
    this.http.get(url, { headers: headers })
      .subscribe(result => {
        return result;
      }, (error: HttpErrorResponse) => {
        this.location.replaceState('/');
        this.router.navigate(['login']);
      }
      );
  }
  private obtenerToken(): boolean {
    const resultToken = sessionStorage.getItem('Token');
    if (resultToken == null) {
      this.location.replaceState('/');
      this.router.navigate(['login']);
      return false;
    } else {
      this.token = JSON.parse(resultToken);
      return true;
    }
  }

  consultar() {
    if (!this.obtenerToken()) {
      return new Observable();
    }
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.token.token
    });
    const url = this.baseUrl + this.apiUrl + '/Consulta';
    return this.http.get(url, { headers: headers });
  }

  registrar(model: User) {
    const body = JSON.stringify(model);
    const params = body;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const url = this.baseUrl + this.apiUrl + '/Crea';
    return this.http.post(url, params, { headers: headers });
  }

  modificar(model: UsuarioModificar) {
    if (!this.obtenerToken()) {
      return;
    }
    const body = JSON.stringify(model);
    const params = body;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + this.token.token
    });
    const url = this.baseUrl + this.apiUrl + '/Modifica';
    return this.http.put(url, params, { headers: headers });
  }

  eliminar(model: User) {
    const body = JSON.stringify(model);
    const params = 'json=' + body;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put('', params, { headers: headers });
  }

}
