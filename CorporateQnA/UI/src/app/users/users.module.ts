import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from "@angular/core";
import { UsersComponent } from "./users.component";
import { UsersRoutingModule } from "./users.routing";
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserCardComponent } from './user-card/user-card.component';
import { AllUsersComponent } from './all-users/all-users.component';

@NgModule({
    imports:[
        UsersRoutingModule,
        SharedModule,
    ],
    declarations:[
        UserCardComponent,
        UsersComponent,
        AllUsersComponent,
        UserDetailsComponent
    ],
    exports:[
        UserCardComponent,
        UsersComponent,
        AllUsersComponent,
        UserDetailsComponent
    ]
})
export class UsersModule{}