export class CategoryDetailsModel{
    id:number;
    name:string;
    description:string;
    thisWeek:number;
    thisMonth:number;
    total:number;
    constructor(args:{}) {
        this.id = args['id']
        this.name= args['name']
        this.description = args['description']
        this.thisWeek = args['thisWeek']
        this.thisMonth = args['thisMonth']
        this.total = args['total']
    }
}