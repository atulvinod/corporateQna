import { AnswerDetailsModel } from './../../models/answer-details.model';
import { AnswerModel } from './../../models/answer.model';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from "@angular/core"
import { AnswerActivityModel } from 'src/models/answer-activity.model';
import { AnswerStateModel } from 'src/models/answer-state.model';
import { GetAnswersModel } from 'src/models/get-answers.model';

@Injectable({
    providedIn: "root"
})
export class AnswerService {
    private httpRoot = "https://localhost:5001"

    constructor(private http: HttpClient) {}

    createAnswer(ans: AnswerModel) {
        return this.http.post(this.httpRoot + "/answer", ans);
    }

    getAnswersForQuestion(query: GetAnswersModel) {
        return this.http.post<AnswerDetailsModel[]>(this.httpRoot + "/answer/list", query);
    }

    createAnswerActivity(activity: AnswerActivityModel) {
        return this.http.post(this.httpRoot + "/activity/answer", activity);
    }

    setAnswerState(state: AnswerStateModel) {
        return this.http.post(this.httpRoot + "/answer/setstate", state);
    }   
}