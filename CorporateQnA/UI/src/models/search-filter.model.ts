export class SearchFilterModel{
    searchInput:string;
    categoryId: number;
    show:number;
    sortBy:number
    userId: number
    constructor(args:{}) {
        this.searchInput = args['searchInput']
        this.categoryId = args['categoryId'] 
        this.show = args['show']
        this.sortBy = args['sortBy']
        this.userId = args['userId'] 
    }
}