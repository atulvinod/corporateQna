export class GetAnswersModel{
    
    questionId:number;
    userId:number;

    constructor(args:{}){
        this.questionId = args['questionId'];
        this.userId = args['userId'];
    }
}