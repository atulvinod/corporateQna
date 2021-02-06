import { SearchFilterModel } from './../../models/search-filter.model';
import { QuestionModel } from './../../models/question.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";
import { QuestionDetailsModel } from 'src/models/question-details.model';
import { QuestionActivityModel } from 'src/models/question-activity.model';

@Injectable({
    providedIn: "root"
})
export class QuestionService {

    private httpRoot = "https://localhost:5001"

    constructor(private http: HttpClient) {}

    createQuestion(question: QuestionModel) {
        return this.http.post(this.httpRoot + "/questions", question);
    }

    getQuestions() {
        return this.http.get<QuestionDetailsModel[]>(this.httpRoot + "/questions");
    }

    createQuestionActivity(activity: QuestionActivityModel) {
        return this.http.post(this.httpRoot + "/activity/question", activity);
    }

    searchQuestion(filter: SearchFilterModel) {
        return this.http.post<QuestionDetailsModel[]>(this.httpRoot + "/questions/search", filter)
    }
}