import {Component, Pipe, PipeTransform} from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: 'halsted-app',
  template: `
    <div class="box">
      <i class="nameOfTheTable1">Spen</i>
      <div class="table-scroll1" id="spen">
        <table>
          <thead>
          <tr>
            <th>Identifier</th>
            <th>Spen</th>
          </tr>
          </thead>
          <tbody>
          <tr *ngFor="let item of retrievedObject2.spenSet | keyvalue">
            <td>{{item.key}}</td>
            <td>{{item.value}}</td>
          </tr>
          </tbody>
          <tfoot>
          <tr>
            <th>Total</th>
            <th></th>
          </tr>
          </tfoot>
        </table>
      </div>
      <i class="nameOfTheTable2">Chepin's Full Metric</i>
      <div class="table-scroll2">
        <table>
          <thead>
          <tr>
            <th>Group of variables</th>
            <th>Variables related to the group</th>
            <th>Number of variables in the group</th>
            <th>Chepin's metric</th>
          </tr>
          </thead>
          <tbody>
          <tr *ngFor="let item of retrievedObject1.chapinTypes | keyvalue">
            <td>{{item.key}}</td>
            <td>{{item.value}}</td>
          </tr>
          </tbody>
          <tfoot>
          <tr>
            <th>Total</th>
            <th></th>
          </tr>
          </tfoot>
        </table>
      </div>
        <p class="resultOfTheMetric">Results of the Chapin and Spen metrics</p>
      </div>
    <button (click)="goBack()" id="btn" class="shine-button">Go back</button>
    `
})

export class ChapinAndSpenComponent {
  retrievedObject1 = JSON.parse(<string>localStorage.getItem('key1'));
  retrievedObject2 = JSON.parse(<string>localStorage.getItem('key2'));

  // Returning to the page with the input of the code for calculating the Halstead metric
  constructor(private router: Router){}
  goBack(){
    this.router.navigate(['']);
  }

}
