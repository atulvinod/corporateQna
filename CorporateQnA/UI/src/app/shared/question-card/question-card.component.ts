import { ActivityTypes } from './../../../models/enum/activity-types.enum';
import { QuestionService } from 'src/app/services/question.service';
import { faChevronUp, faEye } from '@fortawesome/free-solid-svg-icons';
import { Component, Input, OnInit } from '@angular/core';
import { QuestionDetailsModel } from 'src/models/question-details.model';
import * as moment from 'moment';
import { QuestionActivityModel } from 'src/models/question-activity.model';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component
    ({
        selector: "app-question",
        templateUrl: "./question-card.component.html"
    })
export class QuestionCardComponent implements OnInit {

    @Input() question: QuestionDetailsModel

    //Icons
    faChevronUp = faChevronUp
    faEye = faEye

    user: any;
    timeAgo = ""

    constructor(private questionService: QuestionService, private oidc: OidcSecurityService) { }

    ngOnInit() {
        this.oidc.userData$.subscribe(value => {
            this.user = value;
        })

        this.timeAgo = moment(this.question.askedOn).fromNow()
    }

    upvoteActivity() {
        let act = new QuestionActivityModel({
            userId: this.user['userId'],
            questionId: this.question.questionId,
            activityType: ActivityTypes.Like
        })

        this.questionService.createQuestionActivity(act).subscribe(value => {
            if (value != 0) {
                this.question.likeCount++
            }
        })
    }

    viewActivity() {
        //for view activity
        let act = new QuestionActivityModel({
            userId: this.user['userId'],
            questionId: this.question.questionId,
            activityType: ActivityTypes.View
        })

        //send view request
        this.questionService.createQuestionActivity(act).subscribe(response => {
            if (response != 0) {
                this.question.viewCount++;
            }
        })
    }
}