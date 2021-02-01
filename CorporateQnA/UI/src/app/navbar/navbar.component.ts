import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import * as moment from 'moment';

@Component
    ({
        selector: "app-nav",
        templateUrl: "./navbar.component.html"
    })
export class NavbarComponent implements OnInit {

    loggedIn = false;
    userName: string = ""

    constructor(public oidcSecurityService: OidcSecurityService) { }

    ngOnInit() {
        this.oidcSecurityService.isAuthenticated$.subscribe(loginState => {
            
            this.loggedIn = loginState
            if (loginState) {
                this.getUserName();
            }
        })
    }

    login() {
        this.oidcSecurityService.authorize();
    }

    logout() {
        this.oidcSecurityService.logoff();
    }

    getUserName() {
        this.oidcSecurityService.userData$.subscribe(user => {
            if (user != null) {
                this.userName = user["name"]
            }
        })
    }

    getCurrentDate() {
        return moment().format("D MMM YYYY");
    }

}