import { Component } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
@Component
    ({
        selector: "app-answer",
        templateUrl:"./answer.component.html"
    })
export class AnswerComponent{
    
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown

    constructor() {}
}