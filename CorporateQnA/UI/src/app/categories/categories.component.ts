import { CategoryDetailsModel } from './../../models/category-details.model';
import { CategoryModel } from './../../models/category.model';
import { CategoryService } from './../services/category.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, TemplateRef, OnDestroy, OnInit } from '@angular/core';
import { faSearch, faRedo, faPlus } from '@fortawesome/free-solid-svg-icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component
    ({
        selector: "app-categories",
        templateUrl: "./categories.component.html"
    })
export class CategoriesComponent implements OnInit {

    categoryOptions: string[] = ["all", "asp.net", "java", "node.js", "dev ops", "ux design"]
    faSearch = faSearch;
    searchForm: FormGroup;
    newCategoryForm: FormGroup
    faRedo = faRedo;
    faPlus = faPlus;
    modalRef: BsModalRef;
    user: any
    categoryDetails: CategoryDetailsModel[] = []

    constructor(private modalService: BsModalService, private userManager: OidcSecurityService, private categoryService: CategoryService) {
        this.searchForm = new FormGroup({
            search: new FormControl(),
            show: new FormControl("0")
        })

        this.newCategoryForm = new FormGroup({
            categoryName: new FormControl("", [Validators.required]),
            categoryDescription: new FormControl("", [Validators.required])
        })

        this.userManager.userData$.subscribe(value => {
            this.user = value
        })
    }

    ngOnInit() {
        this.categoryService.getCategoryDetails().subscribe(value => {
            this.categoryDetails = value;
        })
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: "custom-modal" });
    }

    createCategory() {
        let createdBy = this.user['userId']
        let name = this.newCategoryForm.get('categoryName').value;
        let description = this.newCategoryForm.get('categoryDescription').value;
        let category: CategoryModel = new CategoryModel({ createdBy, name, description })
        this.categoryService.createCategory(category).subscribe(value => {
            console.log(value);
            this.newCategoryForm.reset();
            this.modalRef.hide();
            let newCategoryDetail: CategoryDetailsModel = new CategoryDetailsModel({ name, description, thisWeek: 0, thisMonth: 0, total: 0, id: value })
            this.categoryDetails.push(newCategoryDetail);
        })
    }
}