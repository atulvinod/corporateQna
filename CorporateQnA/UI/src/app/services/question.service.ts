import { SearchFilterModel } from './../../models/search-filter.model';
import { QuestionModel } from './../../models/question.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";
import { QuestionDetailsModel } from 'src/models/question-details.model';
import { QuestionActivityModel } from 'src/models/question-activity.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: "root"
})
export class QuestionService extends BaseService{

    constructor(private http: HttpClient) {
        super();
    }

    createQuestion(question: QuestionModel) {
        return this.http.post(this.getHttpRoot() + "/questions", question);
    }

    getQuestions() {
        return this.http.get<QuestionDetailsModel[]>(this.getHttpRoot() + "/questions");
    }

    createQuestionActivity(activity: QuestionActivityModel) {
        return this.http.post(this.getHttpRoot() + "/activity/question", activity);
    }

    searchQuestion(filter: SearchFilterModel) {
        return this.http.post<QuestionDetailsModel[]>(this.getHttpRoot() + "/questions/search", filter)
    }
}