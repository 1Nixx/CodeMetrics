import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import {Routes, RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { MainComponent }   from './main.component';
import { AboutComponent }   from './about.component';
import { FormsModule } from "@angular/forms";

// Defining routes
const appRoutes: Routes =[
  { path: '', component: MainComponent},
  { path: 'metric', component: AboutComponent}
];

@NgModule({
  declarations: [
    AppComponent, MainComponent, AboutComponent
  ],
  imports: [
    HttpClientModule, BrowserModule, RouterModule.forRoot(appRoutes), FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

