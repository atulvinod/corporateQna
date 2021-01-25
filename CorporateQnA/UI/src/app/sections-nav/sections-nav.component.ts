import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
@Component
    ({
        selector: "app-sections-nav",
        templateUrl: "./sections-nav.component.html"
    })
export class SectionsNavComponent implements OnInit{
    
    constructor(public router: Router, public activatedRoute: ActivatedRoute) { }

    ngOnInit(){
        this.activatedRoute.url.subscribe(value=>{
            console.log("FROM NAV",value);
        })
        console.log(this.getRoute());
    }

    getRoute(){
        return this.router.url;
    }
}