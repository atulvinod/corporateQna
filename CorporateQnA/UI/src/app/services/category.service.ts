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
        return this.http.get()
    }
}