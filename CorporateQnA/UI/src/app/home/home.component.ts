import { FormControl, FormGroup } from '@angular/forms';
import { Component } from '@angular/core';
import { faSearch, faPlus, faRedo } from '@fortawesome/free-solid-svg-icons';

@Component
    ({
        selector: "app-home",
        templateUrl: "./home.component.html"
    })
export class HomeComponent {
    faSearch = faSearch;
    faPlus = faPlus;
    faRedo = faRedo;
    searchForm: FormGroup

    constructor() {

        this.searchForm = new FormGroup({
            searchInput: new FormControl(""),
            category: new FormControl("0"),
            show: new FormControl("0"),
            sortBy: new FormControl("0")
        })

    }
}