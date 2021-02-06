import { CategoryDetailsModel } from './../../../models/category-details.model';
import { Component, Input } from '@angular/core';
@Component
    ({
        selector: "app-category",
        templateUrl:"./category-card.component.html"
    })
export class CategoryCardComponent{
    @Input() categoryDetail:CategoryDetailsModel
    
    constructor() {}
}