import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SectionsNavComponent } from './sections-nav/sections-nav.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SectionsNavComponent,
  ],
  imports: [
    SharedModule,
    BrowserModule,
    HomeModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
