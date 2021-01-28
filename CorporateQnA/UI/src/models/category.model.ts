export class CategoryModel {
    name: string;
    description: string;
    id: string
    constructor(args: {}) {
        this.name = args['name'];
        this.description = args['description'];
        this.id = args['id']
    }
}