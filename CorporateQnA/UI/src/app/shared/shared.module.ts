import { QuestionCardComponent } from './question-card/question-card.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { KekaSelectComponent } from "./keka-select/keka-select.component";
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
    imports:[
        CommonModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule.forChild()
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
        CommonModule
    ]
})
export class SharedModule{}