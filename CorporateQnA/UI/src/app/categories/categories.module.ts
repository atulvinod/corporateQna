import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesRoutingModule } from './categories.routing';
import { CategoriesComponent } from './categories.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CategoryCardComponent } from './category-card/category-card.component';

@NgModule({
  declarations: [
    CategoriesComponent,
    CategoryCardComponent
  ],
  imports: [
    CategoriesRoutingModule,
    SharedModule,
  ],
  exports: [
    CategoryCardComponent,
    CategoriesComponent
  ]
})
export class CategoriesModule { }
