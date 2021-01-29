import { AnswerModel } from './../../../models/answer.model';
import { QuestionModel } from './../../../models/question.model';
import { AnswerService } from './../../services/answer.service';
import { Component, Input, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { QuestionService } from 'src/app/services/question.service';
import { faExpandAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { QuestionDetailsModel } from 'src/models/question-details.model';
@Component
    ({
        selector: "app-answer-panel",
        templateUrl: "./answer-panel.component.html"
    })
export class AnswerPanelComponent implements OnInit{
    
    @Input() question: QuestionDetailsModel;
    newAnswer: FormGroup;
    userData: any
    answerCount = 0;
    answers: AnswerDetailsModel[] = []
    faExpandAlt = faExpandAlt
    faCompressAlt = faCompressAlt;
    


    constructor(private oidcService: OidcSecurityService, private answerService: AnswerService, private questionService: QuestionService) { }

    ngOnInit(){
        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required]),
        })

        this.oidcService.userData$.subscribe(value => {
            this.userData = value;
            console.log("userdata from answer-panel :", this.userData);
        })
    }

    removeTags(str) {
        if ((str === null) || (str === ''))
            return false;
        else
            str = str.toString();

        // Regular expression to identify HTML tags in  
        // the input string. Replacing the identified  
        // HTML tag with a null string. 
        return str.replace(/(<([^>]+)>)/ig, '');
    }

    postAnswer() {
        let answeredBy = this.userData['userId']
        let questionId = this.question.questionId;
        let content = this.removeTags(this.newAnswer.get('content').value);
        let answerModel: AnswerModel = new AnswerModel({ answeredBy, questionId, content })
        console.log(answerModel);
        this.answerService.createAnswer(answerModel).subscribe(value=>{
            console.log("Created answer ",value);
            this.newAnswer.reset();
        })
    }
}