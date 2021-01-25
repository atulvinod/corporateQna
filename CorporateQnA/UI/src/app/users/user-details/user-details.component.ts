import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FormGroup, FormControl } from '@angular/forms';
import { Component } from '@angular/core';
@Component
    ({
        selector: "app-user-details",
        templateUrl:"./user-details.component.html"
    })
export class UserDetailsComponent{

    searchForm: FormGroup;
    
    constructor() {
        this.searchForm = new FormGroup({
            search: new FormControl()
        })
    }
}