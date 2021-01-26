import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesRoutingModule } from './categories.routing';
import { CategoriesComponent } from './categories.component';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [
    CategoriesComponent
  ],
  imports: [
    CategoriesRoutingModule,
    SharedModule,
  ],
  exports: [
    CategoriesComponent
  ]
})
export class CategoriesModule { }
