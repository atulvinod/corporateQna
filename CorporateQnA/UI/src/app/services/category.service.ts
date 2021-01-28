import { CategoryDetailsModel } from './../../models/category-details.model';
import { CategoryModel } from './../../models/category.model';
import { HttpClient } from "@angular/common/http";
import { Injectable, isDevMode } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class CategoryService{

    httpRoot = "https://localhost:5001"

    constructor(public http: HttpClient) {
        if(!isDevMode){
            this.httpRoot = ""
        }
    }

    getCategories(){
        return this.http.get<CategoryModel[]>(this.httpRoot+"/category")
    }

    createCategory(category: CategoryModel){
        return this.http.post(this.httpRoot+"/category/create",category)
    }

    getCategoryDetails(){
        return this.http.get<CategoryDetailsModel[]>(this.httpRoot+"/category/details")
    }

}