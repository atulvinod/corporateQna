import { UserService } from './../services/user.service';
import { UserDetailsModel } from './../../models/user-details.model';
import { Router, ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Component, OnInit } from '@angular/core';
@Component
    ({
        selector: "app-users",
        templateUrl:"./users.component.html"
    })
export class UsersComponent implements OnInit{

    userDetails:UserDetailsModel;

    constructor() {}

    ngOnInit(){
      
    }
}