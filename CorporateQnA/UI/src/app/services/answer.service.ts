import { AnswerDetailsModel } from './../../models/answer-details.model';
import { AnswerModel } from './../../models/answer.model';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from "@angular/core"

@Injectable({
    providedIn:"root"
})
export class AnswerService{
    httpRoot = "https://localhost:5001"

    constructor(public http: HttpClient) {
        if(!isDevMode){
            this.httpRoot = ""
        }
    }

    createAnswer(ans:AnswerModel){
        return this.http.post(this.httpRoot+"/answer",ans);
    }

    getAnswersForQuestion(questionId: number){
        return this.http.get<AnswerDetailsModel[]>(this.httpRoot+"/answer?qId="+questionId);
    }
}