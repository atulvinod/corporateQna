export class QuestionDetailsModel {

    questionId: number;
    userName: string;
    questionTitle: string;
    content: string;
    askedBy: number;
    askedOn: string;
    categoryId: number;
    likeCount: number;
    viewCount: number;
    resolved: boolean;
    answerCount: number;

    constructor(args: {}) {
        this.questionId = args['questionId']
        this.userName = args['userName']
        this.questionTitle = args['questionTitle']
        this.content = args['content']
        this.askedBy = args['askedBy']
        this.askedOn = args['askedOn']
        this.categoryId = args['categoryId']
        this.likeCount = args['likeCount']
        this.viewCount = args['viewCount']
        this.resolved = args['resolved']
        this.answerCount = args['answerCount']
    }
}