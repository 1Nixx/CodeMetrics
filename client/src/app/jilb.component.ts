import {Component} from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: 'halsted-app',
  template: `
    <div class="box">
    <div class="metricFrameJilb">
      <p>Абсолютная сложность программы CL = <b class="txtOfMetric">{{retrievedObject.amountOfConditionalOperators}}</b></p>
      <p>Относительная сложность программы cl = <b class="txtOfMetric">{{retrievedObject.relativeComplexityOfProgram.toFixed(4)}}</b></p>
      <p>Максимальный уровень вложенности условного оператора CLI = <b class="txtOfMetric">{{retrievedObject.maximumNestingLevel}}</b></p>
    </div>
      <p class="resultOfTheMetric">Results of the Jilb metric</p>
      </div>
    <button (click)="goBack()" id="btn" class="shine-button">Go back</button>
    `
})

export class JilbComponent {
  retrievedObject = JSON.parse(<string>localStorage.getItem('key'));

  // Returning to the page with the input of the code for calculating the Halstead metric
  constructor(private router: Router){}
  goBack(){
    this.router.navigate(['']);
  }

}
