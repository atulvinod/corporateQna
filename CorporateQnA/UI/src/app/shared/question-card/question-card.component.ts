import { faChevronUp, faEye } from '@fortawesome/free-solid-svg-icons';
import { Component } from '@angular/core';
@Component
    ({
        selector: "app-question",
        templateUrl:"./question-card.component.html"
    })
export class QuestionCardComponent{
    faChevronUp= faChevronUp
    faEye = faEye
    constructor() {}

}