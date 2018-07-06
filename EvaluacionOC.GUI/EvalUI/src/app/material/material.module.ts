import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatInputModule, MatFormFieldModule, MatSelectModule, MatToolbarModule,
  MatButtonModule, MatTableModule, MatCheckboxModule, MatCardModule, MatSidenavModule
} from '@angular/material';
import { InputStandarComponent } from './input-standar/input-standar.component';
import { InputEmailComponent } from './input-email/input-email.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputPasswordComponent } from './input-password/input-password.component';
import { SelectComponent } from './select/select.component';
import { DataGridComponent } from './data-grid/data-grid.component';



@NgModule({
  imports: [
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatCheckboxModule,
    MatCardModule,
    MatSidenavModule
  ],
  exports: [
    InputStandarComponent,
    InputEmailComponent,
    InputPasswordComponent,
    SelectComponent,
    MatToolbarModule,
    MatButtonModule,
    DataGridComponent,
    MatCheckboxModule,
    MatCardModule,
    MatSidenavModule
  ],
  declarations: [InputStandarComponent, InputEmailComponent, InputPasswordComponent,
    SelectComponent, DataGridComponent]
})
export class MaterialModule { }
