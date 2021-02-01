import { AnswerDetailsModel } from '../../../models/answer-details.model';
import { AnswerModel } from '../../../models/answer.model';
import { QuestionModel } from '../../../models/question.model';
import { AnswerService } from '../../services/answer.service';
import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { QuestionService } from 'src/app/services/question.service';
import { faExpandAlt, faCompressAlt } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { QuestionDetailsModel } from 'src/models/question-details.model';
import * as moment from 'moment';
import { GetAnswersModel } from 'src/models/get-answers.model';

@Component
    ({
        selector: "app-answer-panel",
        templateUrl: "./answer-panel.component.html"
    })
export class AnswerPanelComponent implements OnInit {

    @Input() question: QuestionDetailsModel;

    //ICONS
    faExpandAlt = faExpandAlt
    faCompressAlt = faCompressAlt;

    //FORM
    newAnswer: FormGroup;

    questionTimeAgo: string
    
    //Initially not set
    solutionAnswerId: number = -1;
    userData: any

    answerCount = 0;
    answers: AnswerDetailsModel[] = []
    fetchquery: GetAnswersModel

    toggleFlyoutEditor = false

    constructor(private oidcService: OidcSecurityService, private answerService: AnswerService) { }

    ngOnInit() {
        this.oidcService.userData$.subscribe(user => {
            this.userData = user;
            this.fetchquery = new GetAnswersModel({ questionId: this.question.questionId, userId: Number(this.userData['userId']) })
        })

        this.newAnswer = new FormGroup({
            content: new FormControl("", [Validators.required, this.editorValidator()]),
        })

        this.answerService.getAnswersForQuestion(this.fetchquery).subscribe(answers => {
            this.answers = answers;
            this.answerCount = answers.length
        })

        this.questionTimeAgo = moment(this.question.askedOn).fromNow()
    }

    ngOnChanges(changes: SimpleChanges) {

        if (this.userData != null) {

            this.fetchquery = new GetAnswersModel({ questionId: this.question.questionId, userId: Number(this.userData['userId']) })

            this.answerService.getAnswersForQuestion(this.fetchquery).subscribe(values => {
                this.answers = values;
                this.answerCount = values.length
            })
        }
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

        this.answerService.createAnswer(answerModel).subscribe(answerId => {
          
            let answerDetail: AnswerDetailsModel = new AnswerDetailsModel({
                answerId,
                likeCount: 0,
                dislikeCount: 0,
                content,
                answeredBy: this.userData['userId'],
                answeredOn: moment(),
                questionId,
                answeredByName: this.userData['name'],
                askedBy: this.question.askedBy,
                isBestSolution: false,
                likedByUser: false,
                dislikedByUser: false
            })

            this.answers.push(answerDetail);
            this.newAnswer.get('content').patchValue("")
            this.answerCount++;
        })
    }

    editorValidator(): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } | null => {
            console.log("validator ", this.removeTags(control.value))
            let empty = this.removeTags(control.value).length == 0

            return empty ? { "empty": "Empty content" } : null;
        };
    }

    questionEvent(event:{questionId:number,resolveState:boolean,answerId:number}){
        this.question.resolved = event.resolveState
        this.solutionAnswerId = event.answerId
    }
}