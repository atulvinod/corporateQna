import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../shared/shared.module';
import { AllUsersComponent } from './view-users/all-users.component';
import { NgModule } from "@angular/core";
import { UsersComponent } from "./users.component";
import { UsersRoutingModule } from "./users.routing";
import { UserDetailsComponent } from './user-details/user-details.component';

@NgModule({
    imports:[
        UsersRoutingModule,
        SharedModule,
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