export class UserDetailsModel {
    id: number;
    name: string;
    email: string;
    department: string;
    location: string;
    position: string;
    totalLikes: number;
    totalDislikes: number;
    questionAsked: number;
    questionAnswered: number;
    questionResolved: number;

    constructor(args: {}) {
        this.id = args['id']
        this.name = args['name']
        this.email = args['email']
        this.department = args['department']
        this.location = args['location']
        this.position = args['position']
        this.totalLikes = args['totalLikes']
        this.totalDislikes = args['totalDislikes']
        this.questionAsked = args['questionAsked']
        this.questionAnswered = args['questionAnswered']
        this.questionResolved = args['questionResolved']
    }
}