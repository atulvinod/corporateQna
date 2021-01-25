import { SharedModule } from './../shared/keka-select/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home.component";
import { HomeRoutingModule } from "./home.routing";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
    imports:[
        HomeRoutingModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        SharedModule
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