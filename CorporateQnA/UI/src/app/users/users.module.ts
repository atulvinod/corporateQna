import { AllUsersComponent } from './view-users/all-users.component';
import { NgModule } from "@angular/core";
import { UsersComponent } from "./users.component";
import { UsersRoutingModule } from "./users.routing";
import { UserDetailsComponent } from './user-details/user-details.component';

@NgModule({
    imports:[
        UsersRoutingModule,
    ],
    declarations:[
        UsersComponent,
        AllUsersComponent,
        UserDetailsComponent
    ],
    exports:[
        UsersComponent,
        AllUsersComponent,
        UserDetailsComponent
    ]
})
export class UsersModule{}