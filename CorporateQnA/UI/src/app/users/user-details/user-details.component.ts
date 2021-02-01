import { QuestionDetailsModel } from 'src/models/question-details.model';
import { UserDetailsModel } from './../../../models/user-details.model';
import { faArrowLeft, faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { forkJoin } from 'rxjs';

@Component
    ({
        selector: "app-user-details",
        templateUrl: "./user-details.component.html"
    })
export class UserDetailsComponent implements OnInit {

    faArrowLeft = faArrowLeft
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown

    userDetails: UserDetailsModel;
    userQuestions: QuestionDetailsModel[] = []
    userAnsweredQuestions: QuestionDetailsModel[] = []
    currentQuestion: QuestionDetailsModel

    currentTab:string = "ALL"
    currentQuestions:QuestionDetailsModel[] = []

    constructor(private router: Router, private activatedRoute: ActivatedRoute, private userService: UserService) {}

    ngOnInit() {
        this.activatedRoute.params.subscribe(routeId => {

            forkJoin([
                this.userService.getUserDetails(Number(routeId['id'])),
                this.userService.getUserQuestions(Number(routeId['id'])),
                this.userService.getUserAnsweredQuestions(Number(routeId['id']))
            ]).subscribe(values=>{
                this.userDetails = values[0]
                this.userQuestions = values[1]
                this.userAnsweredQuestions = values[2]

                this.currentQuestions = this.userQuestions
            })
            
        })
    }

    setTab(tab){
        console.log("Clicked")
        if(tab=="ALL"){
            this.currentQuestions = this.userQuestions;
        }else{
            this.currentQuestions = this.userAnsweredQuestions;
        }
        this.currentTab = tab;
        this.currentQuestion = null;
    }

    viewQuestion(i:QuestionDetailsModel){
        this.currentQuestion = i
    }
}