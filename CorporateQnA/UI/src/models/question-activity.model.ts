import { ActivityTypes } from './enum/activity-types.enum';

export class QuestionActivityModel{
    
    userId:number
    questionId:number;
    activityType:ActivityTypes
    answerId: number
    constructor(args:{}) {
        this.userId = args['userId']
        this.questionId = args['questionId']
        this.activityType = args['activityType']
        this.answerId = args['answerId'];
    }
}