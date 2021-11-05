import {Component, OnInit} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { IHalsteadMetric } from "./HalsteadMetric";
import { Router } from "@angular/router";
import {IJilbMetric} from "./JilbMetric";

@Component({
  selector: 'home-app',
  template: `
      <div class="box" >
        <p class="nameOfTheMetric">Metrics</p>
        <textarea class="input_text" type="text" (keydown)="doTabulation($event)" [(ngModel)]="code"></textarea>
        <button (click)="goCalculateHalstead()" class="shine-button">Calculate the Halstead metric</button>
        <button id="btn1" (click)="goCalculateJilb()" class="shine-button">Calculate the Jilb metric</button>
        <button id="btn2" (click)="goClear()" class="shine-button">Clear the field</button>
        <p class="authors">Created by Nikita Hripach & Pavel Starovoytov</p>
      </div>
  `
})

export class MainComponent {
  code: string | null = sessionStorage.getItem('save');

  goClear() {
    this.code = "";
  }

  // Give possibility of tabulation
  doTabulation(event:any) {
    if (event.key == 'Tab') {
      event.preventDefault();
      let start = event.target.selectionStart;
      let end = event.target.selectionEnd;
      event.target.value = event.target.value.substring(0, start) + '\t' + event.target.value.substring(end);
      event.target.selectionStart = event.target.selectionEnd = start + 1;
    }
  }

  // Going to the page with the calculated Halstead metric
  constructor(private http: HttpClient, private router: Router){ }
  goCalculateHalstead(){
    if (typeof this.code === "string") {
      sessionStorage.setItem('save', this.code);
    }
    // @ts-ignore
    this.http.post<IHalsteadMetric>("https://localhost:5001/api/Halstead", this.code.trim())
      .subscribe((data) =>
      {localStorage.setItem('key', JSON.stringify(data)); this.router.navigate(['/halsted'])})
  };

  // Going to the page with the calculated Jilb metric
  goCalculateJilb() {
    if (typeof this.code === "string") {
      sessionStorage.setItem('save', this.code);
    }
    // @ts-ignore
    this.http.post<IJilbMetric>("https://localhost:5001/api/Jilb", this.code.trim())
      .subscribe((data) =>
      {localStorage.setItem('key', JSON.stringify(data)); console.log(data); this.router.navigate(['/jilb'])})
  }

}
