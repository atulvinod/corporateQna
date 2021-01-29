import { QuestionActivityEnum } from "./enum/question-activity.enum";

export class QuestionActivityModel{
    userId:number
    questionId:number;
    activityType:QuestionActivityEnum
    constructor(args:{}) {
        this.userId = args['userId']
        this.questionId = args['questionId']
        this.activityType = args['activityType']
    }
}