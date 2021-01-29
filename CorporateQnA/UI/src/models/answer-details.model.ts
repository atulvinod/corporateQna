export class AnswerDetailsModel{
    answerId : number;
    likeCount : number
    dislikeCount: number
    content :string;
    answeredBy : number;
    answeredOn :string;
    questionId: number;
    userName :string;

    constructor(args:{}) {
        this.answerId   = args['answerId ']
        this.likeCount  = args['likeCount']
        this.dislikeCount = args['dislikeCount']
        this.content  = args['content']
        this.answeredBy   = args['answeredBy']
        this.answeredOn  = args['answeredOn']
        this.userName   = args['userName']
        this.questionId  = args['questionId']
    }
}