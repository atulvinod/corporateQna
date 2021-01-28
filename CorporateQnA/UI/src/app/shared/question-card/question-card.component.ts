import { faChevronUp, faEye } from '@fortawesome/free-solid-svg-icons';
import { Component, Input, OnInit } from '@angular/core';
import { QuestionDetailsModel } from 'src/models/question-details.model';
import * as moment from 'moment';

@Component
    ({
        selector: "app-question",
        templateUrl:"./question-card.component.html"
    })
export class QuestionCardComponent implements OnInit{
    faChevronUp= faChevronUp
    faEye = faEye
    
    timeAgo = ""

    @Input() question:QuestionDetailsModel
    constructor() {
    }

    ngOnInit(){
        this.timeAgo = moment(this.question.askedOn).fromNow()
    }
}