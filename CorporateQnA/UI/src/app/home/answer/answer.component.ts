import { AnswerDetailsModel } from './../../../models/answer-details.model';
import { Component, Input } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';

@Component
    ({
        selector: "app-answer",
        templateUrl:"./answer.component.html"
    })
export class AnswerComponent{
    
    @Input() answer: AnswerDetailsModel;
    timeAgo:string;
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown;
    

    constructor() {}

    ngOnInit(){
        this.timeAgo = moment(this.answer.answeredOn).fromNow();
    }
}