import { QuestionCardComponent } from './question-card/question-card.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { KekaSelectComponent } from "./keka-select/keka-select.component";
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports:[
        CommonModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule.forChild(),
        HttpClientModule
    ],
    declarations:[
        KekaSelectComponent,
        QuestionCardComponent
    ],
    exports:[
        KekaSelectComponent,
        QuestionCardComponent,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule,
        CommonModule,
        HttpClientModule
    ]
})
export class SharedModule{}