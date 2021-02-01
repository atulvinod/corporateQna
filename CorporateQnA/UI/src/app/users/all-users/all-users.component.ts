import { UserDetailsModel } from '../../../models/user-details.model';
import { UserService } from '../../services/user.service';
import { faSearch, faThumbsDown, faThumbsUp, faTshirt } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
@Component
    ({
        selector: "app-all-users",
        templateUrl:"./all-users.component.html"
    })
export class AllUsersComponent implements OnInit{
    
    searchForm: FormGroup;
    faSearch = faSearch
    thumbsUp = faThumbsUp
    thumbsDown = faThumbsDown
    
    allUsers: UserDetailsModel[] = []
    showUsers : UserDetailsModel[] = []

    constructor(private userService: UserService) {
        this.searchForm = new FormGroup({
            search: new FormControl()
        })
    }

    ngOnInit(){
        this.userService.getAllUsersDetails().subscribe(users=>{
            this.allUsers = users;
            this.showUsers = users;
        })

        this.searchForm.get("search").valueChanges.subscribe(input=>{
            this.showUsers = this.allUsers.filter((e,i,a)=>{
                return new RegExp((input ?? "").replace(".", "\\."), "ig").exec(e.name) != null
            })
        })
    }
}