import { Component, OnInit } from '@angular/core';
import { Genero } from '../../shared/model/genero';
import { ApiServiceService } from '../../shared/services/api-service.service';
import { User } from '../../shared/model/user';

@Component({
  selector: 'ev-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css']
})
export class RegistrarComponent implements OnInit {
  source: Genero[] = [{
    Id: 1,
    Descripcion: 'Masculino'
  }, {
      Id: 2,
      Descripcion: 'Femenino'
    }
  ];
  private btnEnabled: boolean;
  private validador = {
    nUsu: false,
    email: false,
    pass: false,
    genero: false
  };
  constructor(private api: ApiServiceService) { this.btnEnabled = false; }
  usuario: User = {
    nombreUsuario: '',
    email: '',
    generoId: 0,
    id: 0,
    password: '',
    status: false
  };
  ngOnInit() {
  }
  onClick() {
    this.api.registrar(this.usuario).subscribe(result => {
      console.log(result);
    });
  }
  onBlurNU($event) {
    this.usuario.nombreUsuario = $event.value;
    this.validador.nUsu = $event.valid;
    this.validar();
  }
  onBlurEmail($event) {
    this.usuario.email = $event.value;
    this.validador.email = $event.valid;
    
    this.validar();
  }
  onBlurPass($event) {
    this.usuario.password = $event.value;
    this.validador.pass = $event.valid;
    this.validar();
  }
  onSelect($event) {
    
    this.usuario.genero = $event;
    if (this.usuario.genero == null) {
      this.validador.genero = false;
    } else {
      this.validador.genero = true;
      this.usuario.generoId = this.usuario.genero.Id;
    }
    this.validar();
  }
  validar() {
    
    if (this.validador.nUsu && this.validador.email && this.validador.pass && this.validador.genero) {
      this.btnEnabled = true;
    } else {
      this.btnEnabled = false;
    }
  }

}
