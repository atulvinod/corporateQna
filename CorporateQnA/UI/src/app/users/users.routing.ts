import { UsersComponent } from './users.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";
import { UserDetailsComponent } from './user-details/user-details.component';
import { AllUsersComponent } from './all-users/all-users.component';

const routes : Routes = [
    {
        path:"",
        component: UsersComponent,
        children:[
            {
                path:"all",
                component: AllUsersComponent
            },
            {
                path:"view/:id",
                component: UserDetailsComponent
            }
        ]
    }
]

@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
    exports:[
        RouterModule
    ]
})
export class UsersRoutingModule{}