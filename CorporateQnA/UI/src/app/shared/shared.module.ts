import { QuestionCardComponent } from './question-card/question-card.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
    imports:[
        CommonModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule.forChild(),
        HttpClientModule,
        NgSelectModule,
        FormsModule
    ],
    declarations:[
        QuestionCardComponent
    ],
    exports:[
        QuestionCardComponent,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule,
        CommonModule,
        HttpClientModule,
        NgSelectModule,
        FormsModule
    ]
})
export class SharedModule{}