import {EidtorAComponent} from './editor/editor.component';
import { NgxEditorModule } from 'ngx-editor';
import { QuestionCardComponent } from './question-card/question-card.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpClientModule } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';
import {AnswerComponent} from './answer/answer.component';
import {AnswerPanelComponent} from './answer-panel/answer-panel.component'

@NgModule({
    imports:[
        CommonModule,
        ReactiveFormsModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule.forChild(),
        HttpClientModule,
        NgSelectModule,
        FormsModule,
        NgxEditorModule
    ],
    declarations:[
        EidtorAComponent,
        QuestionCardComponent,
        AnswerComponent,
        AnswerPanelComponent
    ],
    exports:[
        QuestionCardComponent,
        FontAwesomeModule,
        ReactiveFormsModule,
        ModalModule,
        CommonModule,
        HttpClientModule,
        NgSelectModule,
        FormsModule,
        AnswerPanelComponent,
        AnswerComponent,
        EidtorAComponent
    ]
})
export class SharedModule{}