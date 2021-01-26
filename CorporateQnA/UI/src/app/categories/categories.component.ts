import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, TemplateRef } from '@angular/core';
import { faSearch, faRedo, faPlus } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component
    ({
        selector: "app-categories",
        templateUrl: "./categories.component.html"
    })
export class CategoriesComponent {
    
    categoryOptions :string[] = ["all","asp.net","java","node.js","dev ops","ux design"]
    faSearch = faSearch;
    searchForm: FormGroup;
    newCategoryForm: FormGroup
    faRedo = faRedo;
    faPlus = faPlus;
    modalRef: BsModalRef;

    constructor(private modalService: BsModalService) {
        this.searchForm = new FormGroup({
            search: new FormControl(),
            show: new FormControl("0")
        })

        this.newCategoryForm =  new FormGroup({
            categoryName : new FormControl("",[Validators.required]),
            categoryDiscription : new FormControl("",[Validators.required])
        })
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template,{class:"add-question"});
    }
}