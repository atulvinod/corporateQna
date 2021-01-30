export class AnswerDetailsModel{
    answerId : number;
    likeCount : number
    dislikeCount: number
    content :string;
    answeredBy : number;
    answeredOn :string;
    isBestSolution: number;
    questionId: number;
    answeredByName :string;
    askedBy: number;
    constructor(args:{}) {
        this.answerId   = args['answerId ']
        this.likeCount  = args['likeCount']
        this.dislikeCount = args['dislikeCount']
        this.content  = args['content']
        this.answeredBy   = args['answeredBy']
        this.answeredOn  = args['answeredOn']
        this.answeredByName   = args['answeredByName']
        this.questionId  = args['questionId']
        this.isBestSolution = args['isBestSolution']
        this.askedBy  = args['askedBy']
    }
}