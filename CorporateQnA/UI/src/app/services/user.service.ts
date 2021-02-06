import { QuestionDetailsModel } from 'src/models/question-details.model';
import { UserDetailsModel } from './../../models/user-details.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class UserService{
    
    private httpRoot = "https://localhost:5001"
    
    constructor(private http: HttpClient) {}

    getAllUsersDetails(){
        return this.http.get<UserDetailsModel[]>(this.httpRoot+"/userdata/all")
    }

    getUserDetails(userId:number){
        return this.http.get<UserDetailsModel>(this.httpRoot+"/userdata/user?userId="+userId)
    }

    getUserQuestions(userId: number){
        return this.http.get<QuestionDetailsModel[]>(this.httpRoot+"/questions/askedBy?userId="+userId)
    }

    getUserAnsweredQuestions(userId: number){
        return this.http.get<QuestionDetailsModel[]>(this.httpRoot+"/questions/answeredBy?userId="+userId)
    }

}