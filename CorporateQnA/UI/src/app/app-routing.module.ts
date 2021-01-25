import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path:"",
    loadChildren:()=>import("./home/home.module").then(m=>m.HomeModule)
  },
  {
    path:"categories",
    loadChildren:()=>import("./categories/categories.module").then(m=>m.CategoriesModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
