export class CategoryModel {
    public name: string;
    public description: string;
    public id: string
    constructor(args: {}) {
        this.name = args['name'];
        this.description = args['description'];
        this.id = args['id']
    }
}