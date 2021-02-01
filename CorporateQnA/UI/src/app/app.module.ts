import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { UsersModule } from './users/users.module';
import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SectionsNavComponent } from './sections-nav/sections-nav.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CategoriesModule } from './categories/categories.module';
import { AuthModule, LogLevel, OidcConfigService } from 'angular-auth-oidc-client';

// https://localhost:44379/

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
    oidcConfigService.withConfig({
      clientId: 'angular',
      stsServer: 'https://localhost:5001',
      responseType: 'code',
      redirectUrl: window.location.origin,
      postLogoutRedirectUri: window.location.origin,
      scope: 'openid profile email IdentityServerApi',
      logLevel: LogLevel.Debug,
    });
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SectionsNavComponent,
  ],
  imports: [
    SharedModule,
    BrowserModule,
    CategoriesModule,
    HomeModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    UsersModule,
    AuthModule.forRoot(),
    ProgressbarModule.forRoot(),
  ],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
