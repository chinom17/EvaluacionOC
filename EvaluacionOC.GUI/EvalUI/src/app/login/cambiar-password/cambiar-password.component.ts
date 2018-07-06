import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from '../../shared/services/api-service.service';
import { UsuarioPassword } from '../../shared/model/usuario-password';

@Component({
  selector: 'ev-cambiar-password',
  templateUrl: './cambiar-password.component.html',
  styleUrls: ['./cambiar-password.component.css']
})
export class CambiarPasswordComponent implements OnInit {

  constructor(private api: ApiServiceService) { }
  valido: false;
  usuario: UsuarioPassword = {
    Password: ''
  };
  ngOnInit() {
    this.api.verificaLogin();
  }
  onClick() {
    this.api.cambiarPassword(this.usuario).subscribe(result => {
      console.log(result);
    });
  }
  onBlurPass($event) {
    this.usuario.Password = $event.value;
    this.valido = $event.valid;
  }
}
