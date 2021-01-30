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
        this.oidcSecurityService.isAuthenticated$.subscribe(value => {
            console.log("FROM NAV ,is authenticated => ", value);
            this.loggedIn = value
            if (value) {
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
        this.oidcSecurityService.userData$.subscribe(value => {
            console.log("NAVBAR USER", value)
            if (value != null) {
                this.userName = value["name"]
            }
        })
    }

    getCurrentDate() {
        return moment().format("D MMM YYYY");
    }

}