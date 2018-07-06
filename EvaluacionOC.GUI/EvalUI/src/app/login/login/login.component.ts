import { Component, OnInit } from '@angular/core';
import { Login } from '../../shared/model/login';
import { ApiServiceService } from '../../shared/services/api-service.service';
import { Token } from '../../shared/model/token';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'ev-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  modelLogin: Login;
  habilitaLogin: boolean;
  constructor(private api: ApiServiceService, private router: Router) { }

  ngOnInit() {
    this.modelLogin = {
      Usuario: '',
      Password: ''
    };
    this.habilitaLogin = false;
  }
  onClick() {

    this.api.login(this.modelLogin).subscribe((result: Token) => {
      sessionStorage.setItem('Token', JSON.stringify(result));
      this.router.navigate(['home']);
    }
    // , (error: HttpErrorResponse) => {
    //   let xqqwe = error.error;
    // }
  );
  }

  onBlurUser($event) {
    this.modelLogin.Usuario = $event.value;
    this.estatusBoton();
  }
  onBlurPass($event) {
    this.modelLogin.Password = $event.value;
    this.estatusBoton();
  }
  estatusBoton() {
    if (this.modelLogin.Usuario !== '' && this.modelLogin.Password !== '') {
      this.habilitaLogin = true;
    } else {
      this.habilitaLogin = false;
    }
  }

}
