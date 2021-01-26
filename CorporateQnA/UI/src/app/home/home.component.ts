import { FormControl, FormGroup } from '@angular/forms';
import { Component } from '@angular/core';
import { faSearch, faPlus, faRedo, faThumbsUp, faThumbsDown } from '@fortawesome/free-solid-svg-icons';

@Component
    ({
        selector: "app-home",
        templateUrl: "./home.component.html"
    })
export class HomeComponent {
    faSearch = faSearch;
    faPlus = faPlus;
    faRedo = faRedo;
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown
    searchForm: FormGroup

    categoryOptions :string[] = ["all","asp.net","java","node.js","dev ops","ux design"]
    showOptions :string[] =["all","my questions","my participation","hot","solved","unsolved"]
    sortByOptions:string[] = ["all","recent","last 10 days","last 30 days"]

    constructor() {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            category: new FormControl("0"),
            show: new FormControl("0"),
            sortBy: new FormControl("0")
        })

    }
}