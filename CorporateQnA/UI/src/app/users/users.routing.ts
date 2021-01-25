import { AllUsersComponent } from './view-users/all-users.component';
import { UsersComponent } from './users.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";
import { UserDetailsComponent } from './user-details/user-details.component';

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
                path:"view",
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