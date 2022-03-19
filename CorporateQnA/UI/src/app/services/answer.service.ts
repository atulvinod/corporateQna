import { AnswerDetailsModel } from './../../models/answer-details.model';
import { AnswerModel } from './../../models/answer.model';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from "@angular/core"
import { AnswerActivityModel } from 'src/models/answer-activity.model';
import { AnswerStateModel } from 'src/models/answer-state.model';
import { GetAnswersModel } from 'src/models/get-answers.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: "root"
})
export class AnswerService extends BaseService{

    constructor(private http: HttpClient) {
        super();
    }

    createAnswer(ans: AnswerModel) {
        return this.http.post(this.getHttpRoot() + "/answer", ans);
    }

    getAnswersForQuestion(query: GetAnswersModel) {
        return this.http.post<AnswerDetailsModel[]>(this.getHttpRoot() + "/answer/list", query);
    }

    createAnswerActivity(activity: AnswerActivityModel) {
        return this.http.post(this.getHttpRoot() + "/activity/answer", activity);
    }

    setAnswerAsSolution(state: AnswerStateModel) {
        return this.http.post(this.getHttpRoot() + "/answer/setsolution", state);
    }   
}