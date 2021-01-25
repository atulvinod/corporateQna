import { faSearch, faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
@Component
    ({
        selector: "app-all-users",
        templateUrl:"./all-users.component.html"
    })
export class AllUsersComponent{
    
    searchForm: FormGroup;
    faSearch = faSearch
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown
    
    constructor() {
        this.searchForm = new FormGroup({
            search: new FormControl()
        })
    }
}