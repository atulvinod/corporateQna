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

    //ICONS
    faRedo = faRedo;
    faPlus = faPlus;
    faSearch = faSearch;
    
    searchForm: FormGroup;
    newCategoryForm: FormGroup
  
    modalRef: BsModalRef;
    user: any
    allCategories: CategoryDetailsModel[] = []
    showCategories: CategoryDetailsModel[] = []
    show

    constructor(private modalService: BsModalService, private userManager: OidcSecurityService, private categoryService: CategoryService) {
        this.searchForm = new FormGroup({
            search: new FormControl(),
            show: new FormControl("0")
        })

        this.newCategoryForm = new FormGroup({
            categoryName: new FormControl("", [Validators.required]),
            categoryDescription: new FormControl("", [Validators.required])
        })

        this.userManager.userData$.subscribe(user => {
            this.user = user
        })
    }

    ngOnInit() {
        this.categoryService.getCategoryDetails().subscribe(categories => {
            this.allCategories = categories;
            this.showCategories = categories;
        })

        this.searchForm.valueChanges.subscribe(value => {

            this.showCategories = this.allCategories.filter((e, i, a) => {
                let selected = true;
                if ((value['search'] ?? "").length != 0) {
                    selected = new RegExp(value['search'].replace(".", "\\."), "ig").exec(e.name) != null
                }
                return selected;
            })
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

            let newCategoryDetail: CategoryDetailsModel = new CategoryDetailsModel({ name, description, thisWeek: 0, thisMonth: 0, total: 0, id: value })

            this.allCategories.push(newCategoryDetail);
            this.showCategories.push(newCategoryDetail)

            this.newCategoryForm.reset();
            this.modalRef.hide();

        })
    }

    resetForm() {
        this.searchForm.reset();
        this.searchForm.get('show').patchValue(0)
    }
}