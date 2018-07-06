import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from '../shared/services/api-service.service';
import { User } from '../shared/model/user';
import { Genero } from '../shared/model/genero';
import { UsuarioModificar } from '../shared/model/usuario-modificar';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'ev-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private api: ApiServiceService, private datePipe: DatePipe) { }
  dataSource: User[];
  refresh = false;
  selectedRow = -1;
  usuario: User
    = {
    nombreUsuario: '',
    email: '',
    generoId: 0,
    id: 0,
    password: '',
    status: false
  };
  private usuarioModificar: UsuarioModificar = {
    NombreUsuario: '',
    Id: 0,
    Email: '',
    GeneroId: 0
  };
  private validador = {
    nUsu: false,
    email: false,
    genero: false
  };
  private btnEnabled: boolean;
  generos: Genero[] = [{
    Id: 1,
    Descripcion: 'Masculino'
  },
  {
    Id: 2,
    Descripcion: 'Femenino'
  }];
  ngOnInit() {
    this.api.verificaLogin();
    this.consultarUsuarios();
  }
  onSelected($event) {
    this.usuario = $event;
    this.usuarioModificar = {
      NombreUsuario: this.usuario.nombreUsuario,
      Email: this.usuario.email,
      Id: this.usuario.id,
      GeneroId: this.usuario.generoId
    };
    this.validador.email = true;
    this.validador.nUsu = true;
    this.validador.genero = true;
  }
  onClickMod() {
    this.api.modificar(this.usuarioModificar).subscribe(result => {
      this.consultarUsuarios();
      this.selectedRow = -1;
      this.usuario = {
      nombreUsuario: '',
      email: '',
      generoId: 0,
      id: 0,
      password: '',
      status: false
    };
    });
  }

  onBlurNU($event) {
    this.usuarioModificar.NombreUsuario = $event.value;
    this.validador.nUsu = $event.valid;
    this.validar();
  }
  onBlurEmail($event) {
    this.usuarioModificar.Email = $event.value;
    this.validador.email = $event.valid;
    this.validar();
  }
  onSelect($event) {
    const genero: Genero = $event;
    if (genero == null) {
      this.validador.genero = false;
    } else {
      this.validador.genero = true;
      this.usuarioModificar.GeneroId = genero.Id;
    }
    this.validar();
  }

  validar() {
    if ((this.validador.nUsu && this.validador.email && this.validador.genero) &&
          (this.usuario.nombreUsuario !== this.usuarioModificar.NombreUsuario ||
          this.usuario.email !== this.usuarioModificar.Email ||
          this.usuario.generoId !== this.usuarioModificar.GeneroId
        )) {
      this.btnEnabled = true;
    } else {
      this.btnEnabled = false;
    }
  }
  consultarUsuarios(): any {
    this.api.consultar().subscribe((result: User[]) => {
      this.dataSource = result;
      this.dataSource.forEach(d => {
        d.genero = this.generos.find(g => g.Id === d.generoId);
        d.generoDesc = d.genero.Descripcion;
        d.strFechaCreacion = this.datePipe.transform(d.fechaCreacion, 'dd-MM-yyyy');
        if (d.status) { d.statusDesc = 'Si'; } else { d.statusDesc = 'No'; }

      });
    });
  }

}
