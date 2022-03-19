import { CategoryDetailsModel } from './../../models/category-details.model';
import { CategoryModel } from './../../models/category.model';
import { HttpClient } from "@angular/common/http";
import { Injectable, isDevMode } from "@angular/core";
import { BaseService } from './base.service';

@Injectable({
    providedIn: "root"
})
export class CategoryService extends BaseService{

    constructor(private http: HttpClient) {
        super();
    }

    getCategories() {
        return this.http.get<CategoryModel[]>(this.getHttpRoot() + "/category")
    }

    createCategory(category: CategoryModel) {
        return this.http.post(this.getHttpRoot() + "/category/create", category)
    }

    getCategoryDetails() {
        return this.http.get<CategoryDetailsModel[]>(this.getHttpRoot() + "/category/details")
    }

}