import { AnswerDetailsModel } from './../../../models/answer-details.model';
import { AnswerModel } from './../../../models/answer.model';
import { QuestionModel } from './../../../models/question.model';
import { AnswerService } from './../../services/answer.service';
import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { QuestionService } from 'src/app/services/question.service';
import { faExpandAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { QuestionDetailsModel } from 'src/models/question-details.model';
import * as moment from 'moment';
import { ThrowStmt } from '@angular/compiler';

@Component
    ({
        selector: "app-answer-panel",
        templateUrl: "./answer-panel.component.html"
    })
export class AnswerPanelComponent implements OnInit{
    
    @Input() question: QuestionDetailsModel;
    questionTimeAgo:string
    newAnswer: FormGroup;
    userData: any
    answerCount = 0;
    answers: AnswerDetailsModel[] = []
    faExpandAlt = faExpandAlt
    faCompressAlt = faCompressAlt;
    toggleFlyoutEditor = false
    
    constructor(private oidcService: OidcSecurityService, private answerService: AnswerService) { }

    ngOnInit(){
        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required,this.editorValidator()]),
        })

        this.oidcService.userData$.subscribe(value => {
            this.userData = value;
            console.log("userdata from answer-panel :", this.userData);
        })

        this.answerService.getAnswersForQuestion(this.question.questionId).subscribe(values=>{
            this.answers  = values;
            this .answerCount = values.length
        })

        this.questionTimeAgo = moment(this.question.askedOn).fromNow()

        this.newAnswer.get("content").valueChanges.subscribe(value=>{
            console.log("valid form",this.newAnswer.valid, value);
        })
    }

    ngOnChanges(changes:SimpleChanges){
        this.answerService.getAnswersForQuestion(this.question.questionId).subscribe(values=>{
            this.answers  = values;
            this.answerCount = values.length
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

        this.answerService.createAnswer(answerModel).subscribe(value=>{
            let answerDetail:AnswerDetailsModel = new AnswerDetailsModel({
                answerId : value,
                likeCount:0,
                dislikeCount : 0,
                content,
                answeredBy,
                answeredOn : moment(),
                questionId,
                answeredByName : this.userData['name']
            })
            this.answers.push(answerDetail);
            this.newAnswer.reset();
            this.answerCount++;
        })
    }

    editorValidator(): ValidatorFn {
        return (control: AbstractControl): {[key: string]: any} | null => {
            console.log("validator ",this.removeTags(control.value))
          let empty = this.removeTags(control.value).length == 0

          return empty ? {"empty": "Empty content"} : null;
        };
    }
}