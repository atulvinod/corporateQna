import { AnswerActivityEnum } from './enum/answer-activity.enum';
export class AnswerActivityModel{
    userId:number
    answerId:number;
    activityType:AnswerActivityEnum;
    constructor(args:{}) {
        this.userId = args['userId'];
        this.answerId = args['answerId'];
        this.activityType = args['activityType']
    }
}