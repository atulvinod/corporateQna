export class AnswerModel{
    answeredBy:number;
    questionId:number;
    content:string;
    constructor(args:{}){
        this.answeredBy = args['answeredBy'];
        this.questionId = args['questionId'];
        this.content = args['content'];
        
    }
}