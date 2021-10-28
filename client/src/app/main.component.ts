import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { IHalsteadMetric } from "./HalsteadMetric";
import { Router } from "@angular/router";

@Component({
  selector: 'home-app',
  template: `
      <div class="box" >
        <p class="nameOfTheMetric">Halstead metric</p>
        <textarea class="input_text" type="text" (keydown)="doTabulation($event)" [(ngModel)]="code" (load)="handleLoad($event)"></textarea>
        <button (click)="goCalculate()" class="shine-button">Calculate the Halstead metric</button>
        <p class="authors">Created by Nikita Hripach & Pavel Starovoytov</p>
      </div>
  `
})

export class MainComponent {
  code: string = "";

  handleLoad(event:any){
    if (localStorage.getItem(event) === null) {
      return "";
    }
    return localStorage.getItem(event);
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
  goCalculate(){
    localStorage.setItem('save', this.code);
    this.http.post<IHalsteadMetric>("https://localhost:5001/api/Halstead", this.code.trim())
      .subscribe((data) =>
      {localStorage.setItem('key', JSON.stringify(data)); this.router.navigate(['/metric'])})
  };

}
