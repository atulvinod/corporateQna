export class CategoryModel {
    
    public name: string;
    public description: string;
    public id: string;
    public createdBy: string;

    constructor(args: {}) {
        this.name = args['name'];
        this.description = args['description'];
        this.id = args['id']
        this.createdBy = args['createdBy']
    }
}