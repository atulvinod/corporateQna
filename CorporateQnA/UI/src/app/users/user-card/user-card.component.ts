import { UserDetailsModel } from './../../../models/user-details.model';
import { Component, Input } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
@Component
    ({
        selector: "app-user-card",
        templateUrl:"./user-card.component.html"
    })
export class UserCardComponent{
    
    @Input() userDetail : UserDetailsModel;

    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown
    
    constructor() {}
}