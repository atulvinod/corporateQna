
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home.component";
import { HomeRoutingModule } from "./home.routing";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedModule } from '../shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
    imports:[
        HomeRoutingModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        SharedModule,
        ModalModule.forChild()
    ],
    declarations:[
        HomeComponent
    ],
    exports:[
        HomeComponent
    ]
})
export class HomeModule{

}