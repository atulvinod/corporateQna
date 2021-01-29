import { AnswerActivityModel } from './../../../models/answer-activity.model';
import { AnswerActivityEnum } from './../../../models/enum/answer-activity.enum';
import { AnswerService } from './../../services/answer.service';
import { AnswerDetailsModel } from './../../../models/answer-details.model';
import { Component, Input } from '@angular/core';
import { faThumbsDown, faThumbsUp } from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component
    ({
        selector: "app-answer",
        templateUrl: "./answer.component.html"
    })
export class AnswerComponent {

    @Input() answer: AnswerDetailsModel;
    timeAgo: string;
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown;
    user: any

    constructor(private answerService: AnswerService, private oidc: OidcSecurityService) { }

    ngOnInit() {
        this.timeAgo = moment(this.answer.answeredOn).fromNow();
        this.oidc.userData$.subscribe(value => {
            this.user = value;
        })
    }

    createAnswerActivity(activityType: AnswerActivityEnum) {
        let act = new AnswerActivityModel({
            userId: this.user['userId'],
            answerId: this.answer.answerId,
            activityType
        })

        this.answerService.createAnswerActivity(act).subscribe(value => {
            switch (value) {
                //newly created entry, user was neutral before
                case 1:
                    if (activityType == AnswerActivityEnum.Like) {
                        this.answer.likeCount++;
                    } else {
                        this.answer.dislikeCount++;
                    }
                    break;
                //like was changed to dislike
                case 2:
                    this.answer.likeCount--;
                    this.answer.dislikeCount++;
                    break;
                //dislike was changed to like
                case 3:
                    this.answer.likeCount++;
                    this.answer.dislikeCount--;
                    break;
            }
        })
    }
}