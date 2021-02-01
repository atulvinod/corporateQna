import { SearchFilterModel } from './../../models/search-filter.model';
import { QuestionModel } from './../../models/question.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";
import { QuestionDetailsModel } from 'src/models/question-details.model';
import { QuestionActivityModel } from 'src/models/question-activity.model';
import { QuestionSolutionModel } from 'src/models/question-solution.model';

@Injectable({
    providedIn: "root"
})
export class QuestionService {

    httpRoot = "https://localhost:5001"

    constructor(public http: HttpClient) {
        if (!isDevMode) {
            this.httpRoot = ""
        }
    }

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

    setQuestionSolution(solution: QuestionSolutionModel){
        return this.http.post(this.httpRoot+"/questions/setsolution",solution)
    }
}