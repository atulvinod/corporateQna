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

    public getCategories(){
        return this.http.get<CategoryModel[]>(this.httpRoot+"/category")
    }
}