import { QuestionDetailsModel } from 'src/models/question-details.model';
import { UserDetailsModel } from './../../models/user-details.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from "@angular/core";
import { BaseService } from './base.service';

@Injectable({
    providedIn: "root"
})
export class UserService extends BaseService{
    
    constructor(private http: HttpClient) {
        super();
    }

    getAllUsersDetails(){
        return this.http.get<UserDetailsModel[]>(this.getHttpRoot()+"/userdata/all")
    }

    getUserDetails(userId:number){
        return this.http.get<UserDetailsModel>(this.getHttpRoot()+"/userdata/user?userId="+userId)
    }

    getUserQuestions(userId: number){
        return this.http.get<QuestionDetailsModel[]>(this.getHttpRoot()+"/questions/askedBy?userId="+userId)
    }

    getUserAnsweredQuestions(userId: number){
        return this.http.get<QuestionDetailsModel[]>(this.getHttpRoot()+"/questions/answeredBy?userId="+userId)
    }

}