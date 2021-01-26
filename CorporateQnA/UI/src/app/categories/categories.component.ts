import { FormGroup, FormControl } from '@angular/forms';
import { Component } from '@angular/core';
import { faSearch, faRedo, faPlus } from '@fortawesome/free-solid-svg-icons';
@Component
    ({
        selector: "app-categories",
        templateUrl: "./categories.component.html"
    })
export class CategoriesComponent {
    
    categoryOptions :string[] = ["all","asp.net","java","node.js","dev ops","ux design"]
    faSearch = faSearch;
    searchForm: FormGroup
    faRedo = faRedo;
    faPlus = faPlus;

    constructor() {
        this.searchForm = new FormGroup({
            search: new FormControl(),
            show: new FormControl("0")
        })
    }
}