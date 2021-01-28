export class QuestionModel{
    askedBy:number
    categoryId:number
    content:string
    title:string
    constructor(args:{}) {
        this.askedBy = args['askedBy']
        this.categoryId = args['categoryId']
        this.content = args['content']
        this.title = args['title']
    }
}