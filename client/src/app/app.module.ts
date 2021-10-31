import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import {Routes, RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { MainComponent }   from './main.component';
import { FormsModule } from "@angular/forms";
import { HalstedComponent } from "./halsted.component";
import {JilbComponent} from "./jilb.component";

// Defining routes
const appRoutes: Routes =[
  { path: '', component: MainComponent},
  { path: 'halsted', component: HalstedComponent},
  { path: 'jilb', component: JilbComponent }
];

@NgModule({
  declarations: [
    AppComponent, MainComponent, HalstedComponent, JilbComponent
  ],
  imports: [
    HttpClientModule, BrowserModule, RouterModule.forRoot(appRoutes), FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

