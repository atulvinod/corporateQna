import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { ProgressbarConfig } from 'ngx-bootstrap/progressbar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [{ provide: ProgressbarConfig, useFactory: getProgressbarConfig }]
})
export class AppComponent implements OnInit {
  
  title = 'CorporateQnA';
  showPreloader = true;

  constructor(private oidc: OidcSecurityService) {}

  ngOnInit() {
    this.oidc.checkAuth().subscribe(value => {
      if (value == false) {
        this.oidc.authorize()
      } else {
        this.oidc.userData$.subscribe(value => {
          if (value != null) {
            this.showPreloader = false
          }
        })
      }
    })
  }
}

export function getProgressbarConfig(): ProgressbarConfig {
  return Object.assign(new ProgressbarConfig(), { animate: true, striped: true,  max: 100 });
}
