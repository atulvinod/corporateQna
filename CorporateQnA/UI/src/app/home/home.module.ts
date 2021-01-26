import { AnswerComponent } from './answer/answer.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home.component";
import { HomeRoutingModule } from "./home.routing";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedModule } from '../shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { EidtorAComponent } from './editor/editor.component';
import { NgxEditorModule } from 'ngx-editor';

@NgModule({
    imports: [
        HomeRoutingModule,
        SharedModule,
        NgxEditorModule,
        FormsModule
    ],
    declarations: [
        EidtorAComponent,
        HomeComponent,
        AnswerComponent
    ],
    exports: [
        AnswerComponent,
        EidtorAComponent,
        HomeComponent
    ]
})
export class HomeModule {

}