import { QuestionModel } from './../../models/question.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";

@Injectable({
    providedIn:"root"
})
export class QuestionService{

    httpRoot = "https://localhost:5001"

    constructor(public http: HttpClient) {
        if(!isDevMode){
            this.httpRoot = ""
        }
    }

    createQuestion(question:QuestionModel){
        return this.http.post(this.httpRoot+"/question",question);
    }
}