import { ActivityTypes } from '../../../models/enum/activity-types.enum';
import { QuestionActivityModel } from 'src/models/question-activity.model';
import { QuestionService } from 'src/app/services/question.service';
import { FormGroup, FormControl } from '@angular/forms';
import { AnswerActivityModel } from '../../../models/answer-activity.model';
import { AnswerService } from '../../services/answer.service';
import { AnswerDetailsModel } from '../../../models/answer-details.model';
import { Component, EventEmitter, Input, Output } from '@angular/core';
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
    @Input() isQuestionResolved: boolean = false;

    @Output() setQuestionResolvedState: EventEmitter<{ answerId: number, questionId: number, resolveState: boolean }> = new EventEmitter();

    //ICONS
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown;

    timeAgo: string;

    user: any
    setAsSolution: FormGroup;

    constructor(private answerService: AnswerService, private oidc: OidcSecurityService, private questionService: QuestionService) { }

    ngOnInit() {
        this.timeAgo = moment(this.answer.answeredOn).fromNow();

        this.oidc.userData$.subscribe(user => {
            this.user = user;
        })

        this.setAsSolution = new FormGroup({
            isBestSolution: new FormControl()
        })

        this.setAsSolution.get('isBestSolution').patchValue(this.answer.isBestSolution)

        this.setAsSolution.valueChanges.subscribe(values => {

            this.answer.isBestSolution = values.isBestSolution

            let questionSolvedActivity: QuestionActivityModel = new QuestionActivityModel({
                questionId: this.answer.questionId,
                answerId: this.answer.answerId,
                userId: Number(this.answer.answeredBy),
                activityType : ActivityTypes.Resolved
            })

            this.questionService.createQuestionActivity(questionSolvedActivity).subscribe(() => {
                this.setQuestionResolvedState.emit({
                    questionId: this.answer.questionId,
                    resolveState: values.isBestSolution,
                    answerId: this.answer.answerId
                })
            })
        })
    }

    createAnswerActivity(activityType: ActivityTypes) {

        let activity = new AnswerActivityModel({
            userId: this.user['userId'],
            answerId: this.answer.answerId,
            activityType
        })

        this.answerService.createAnswerActivity(activity).subscribe(value => {
            switch (value) {
                //set the state to neutral
                case 0:
                    if (this.answer.likedByUser) {
                        this.answer.likedByUser = false;
                        this.answer.likeCount = this.answer.likeCount - 1 > 0 ? this.answer.likeCount - 1 : 0;
                    } else {
                        this.answer.dislikedByUser = false;
                        this.answer.dislikeCount = this.answer.dislikeCount - 1 > 0 ? this.answer.dislikeCount - 1 : 0;
                    }
                    break;
                //newly created activity, user was neutral before
                case 1:
                    if (activityType == ActivityTypes.Like) {
                        this.answer.likeCount++;
                        this.answer.likedByUser = true;
                    } else {
                        this.answer.dislikeCount++;
                        this.answer.dislikedByUser = true;
                    }
                    break;
                //like was changed to dislike
                case 2:
                    this.answer.likeCount = this.answer.likeCount - 1 > 0 ? this.answer.likeCount - 1 : 0;
                    this.answer.likedByUser = false;
                    this.answer.dislikeCount++;
                    this.answer.dislikedByUser = true;
                    break;
                //dislike was changed to like
                case 3:
                    this.answer.likeCount++;
                    this.answer.likedByUser = true;
                    this.answer.dislikeCount = this.answer.dislikeCount - 1 > 0 ? this.answer.dislikeCount - 1 : 0;;
                    this.answer.dislikedByUser = false;
                    break;
            }
        })
    }
}