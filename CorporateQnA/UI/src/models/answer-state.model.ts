export class AnswerStateModel {
    
    answerId: number;
    isBestSolution: boolean;
    questionId: number;
    userId:number;

    constructor(args: {}) {
        this.answerId = args['answerId'];
        this.isBestSolution = args['isBestSolution']
        this.questionId = args['questionId']
        this.userId = args['userId']
    }
}