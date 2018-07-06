import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { User } from '../../shared/model/user';
import { ApiServiceService } from '../../shared/services/api-service.service';
import { Genero } from '../../shared/model/genero';


@Component({
  selector: 'ev-data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', display: 'none' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})


export class DataGridComponent implements OnInit {

  columnsToDisplay = [{ name: 'id', display: 'Id' }, { name: 'nombreUsuario', display: 'Nombre de usuario' }
    // tslint:disable-next-line:max-line-length
    , { name: 'email', display: 'Email' }, { name: 'statusDesc', display: 'Activo' },
  { name: 'generoDesc', display: 'Sexo' }, { name: 'genero', display: 'genero' },
  { name: 'strFechaCreacion', display: 'Fecha de creacion' }];
  columnsNames = ['id', 'nombreUsuario', 'email', 'statusDesc', 'generoDesc', 'strFechaCreacion'];
  generos: Genero[] = [{
    Id: 1,
    Descripcion: 'Masculino'
  },
  {
    Id: 2,
    Descripcion: 'Femenino'
  }];
  @Input()
  selectedElement: User;
  @Input()
  selectedRowIndex = -1;
  habilitado: boolean;
  textoBoton: string;
  modificando: boolean;
  @Input()
  dataSource: User[];
  @Output()
  seleccion = new EventEmitter<User>();
  constructor(private api: ApiServiceService) {
    this.modificando = false;
  }

  ngOnInit() {
    this.textoBoton = 'Modificar';
    // this.consultarUsuarios();
  }
  // consultarUsuarios(): any {
  //   this.api.consultar().subscribe((result: User[]) => {
  //     this.dataSource = result;
  //     this.dataSource.forEach(d => {
  //       d.genero = this.generos.find(g => g.Id === d.generoId);
  //       d.generoDesc = d.genero.Descripcion;
  //       d.strFechaCreacion = this.datePipe.transform(d.fechaCreacion, 'dd-MM-yyyy');
  //       if (d.status) { d.statusDesc = 'Si'; } else { d.statusDesc = 'No'; }

  //     });
  //   });
  // }

  onClick($event, element: User) {
    this.selectedElement = element;
    this.selectedRowIndex = element.id;
    this.seleccion.emit(this.selectedElement);
  }
}
