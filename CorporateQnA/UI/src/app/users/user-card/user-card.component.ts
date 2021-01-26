import { Component } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
@Component
    ({
        selector: "app-user-card",
        templateUrl:"./user-card.component.html"
    })
export class UserCardComponent{

    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown
    
    constructor() {}
}