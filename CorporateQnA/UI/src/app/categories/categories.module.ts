import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesRoutingModule } from './categories.routing';
import { CategoriesComponent } from './categories.component';



@NgModule({
  declarations: [
    CategoriesComponent
  ],
  imports: [
    CommonModule,
    CategoriesRoutingModule
  ],
  exports:[
    CategoriesComponent
  ]
})
export class CategoriesModule { }
