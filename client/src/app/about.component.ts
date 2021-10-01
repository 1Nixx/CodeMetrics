import { Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'about-app',
  template: `
      <div class="box">
        <i class="nameOfTheTable1">Operators</i>
        <div class="table-scroll1">
          <table>
            <thead>
            <tr>
              <th>Value</th>
              <th>Num of repetitions</th>
            </tr>
            </thead>
            <tbody>
            <tr *ngFor="let item of retrievedObject.listOfOperators">
              <td>{{item.valueText}}</td>
              <td>{{item.numOfRep}}</td>
            </tr>
            </tbody>
            <tfoot>
            <tr>
              <th>Total</th>
              <th>{{retrievedObject.totalNumOfOperators}}</th>
            </tr>
            </tfoot>
          </table>
        </div>
        <i class="nameOfTheTable2">Operands</i>
        <div class="table-scroll2">
          <table>
            <thead>
            <tr>
              <th>Value</th>
              <th>Num of repetitions</th>
            </tr>
            </thead>
            <tbody>
            <tr *ngFor="let item of retrievedObject.listOfOperands">
              <td>{{item.valueText}}</td>
              <td>{{item.numOfRep}}</td>
            </tr>
            </tbody>
            <tfoot>
            <tr>
              <th>Total</th>
              <th>{{retrievedObject.totalNumOfOperands}}</th>
            </tr>
            </tfoot>
          </table>
        </div>
        <div class="metricFrame">
            <p>Число уникальных операндов программы = <b class="txtOfMetric">{{retrievedObject.numOfUniqueOperands}}</b></p>
            <p>Число уникальных операторов программы = <b class="txtOfMetric">{{retrievedObject.numOfUniqueOperators}}</b></p>
            <p>Общее число операндов в программе = <b class="txtOfMetric">{{retrievedObject.totalNumOfOperands}}</b></p>
            <p>Общее число операторов в программе = <b class="txtOfMetric">{{retrievedObject.totalNumOfOperators}}</b></p>
            <p>Словарь программы =
              <b class="txtOfMetric">{{retrievedObject.numOfUniqueOperands}}</b> +
              <b class="txtOfMetric">{{retrievedObject.numOfUniqueOperators}}</b> =
              <b class="txtOfMetric">{{retrievedObject.dictionaryOfProgram}}</b>
            </p>
            <p>Длина программы =
              <b class="txtOfMetric">{{retrievedObject.totalNumOfOperands}}</b> +
              <b class="txtOfMetric">{{retrievedObject.totalNumOfOperators}}</b> =
              <b class="txtOfMetric">{{retrievedObject.lenOfProgram}}</b>
            </p>
            <p>Объём программы =
              <b class="txtOfMetric">{{retrievedObject.lenOfProgram}}</b> +
              log2(<b class="txtOfMetric">{{retrievedObject.dictionaryOfProgram}}</b>) =
              <b class="txtOfMetric">{{retrievedObject.volumeOfProgram}}</b>
            </p>
        </div>
        <p class="resultOfTheMetric">Results of the Halstead metric</p>
        <button (click)="goBack()" id="btn" class="shine-button">Go back</button>
      </div>
    `
})

export class AboutComponent {
  retrievedObject = JSON.parse(<string>localStorage.getItem('key'));

  // Returning to the page with the input of the code for calculating the Halstead metric
  constructor(private router: Router){}
  goBack(){
    this.router.navigate(['']);
  }

}

