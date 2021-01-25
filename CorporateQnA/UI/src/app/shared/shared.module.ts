import { QuestionCardComponent } from './question-card/question-card.component';

import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { KekaSelectComponent } from "./keka-select/keka-select.component";


@NgModule({
    imports:[
        CommonModule,
        ReactiveFormsModule,
        FontAwesomeModule
    ],
    declarations:[
        KekaSelectComponent,
        QuestionCardComponent
    ],
    exports:[
        KekaSelectComponent,
        QuestionCardComponent
    ]
})
export class SharedModule{}