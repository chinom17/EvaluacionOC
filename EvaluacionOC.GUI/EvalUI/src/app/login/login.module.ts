import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from '../material/material.module';
import { RegistrarComponent } from './registrar/registrar.component';
import { CambiarPasswordComponent } from './cambiar-password/cambiar-password.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ],
  declarations: [LoginComponent, RegistrarComponent, CambiarPasswordComponent]
})
export class LoginModule { }
