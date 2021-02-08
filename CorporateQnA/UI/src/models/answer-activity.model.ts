import { ActivityTypes } from './enum/activity-types.enum';
export class AnswerActivityModel{
    
    userId:number
    answerId:number;
    activityType:ActivityTypes;

    constructor(args:{}) {
        this.userId = args['userId'];
        this.answerId = args['answerId'];
        this.activityType = args['activityType']
    }
}