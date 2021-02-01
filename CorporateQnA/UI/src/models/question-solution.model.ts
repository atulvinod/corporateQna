export class QuestionSolutionModel{
    questionId:number;
    answerId:number;
    resolvedBy:number;
    constructor(args:{}){
        this.questionId = args['questionId'];
        this.answerId = args['answerId'];
        this.resolvedBy = args['resolvedBy'];
    }
}