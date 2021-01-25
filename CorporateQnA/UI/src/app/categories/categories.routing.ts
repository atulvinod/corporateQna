import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";
import { CategoriesComponent } from './categories.component';

const routes:Routes = [
    {
        path:"categories",
        component: CategoriesComponent
    }
]
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoriesRoutingModule{}