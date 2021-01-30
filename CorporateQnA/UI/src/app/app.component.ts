import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit{
  title = 'CorporateQnA';

  constructor(private oidc:OidcSecurityService) {
    
  }

  ngOnInit(){
    this.oidc.checkAuth().subscribe(value=>{
      console.log("APP COMPONENT ",value)
      if(value==false){
        this.oidc.authorize()
      }
    })
  }
}
