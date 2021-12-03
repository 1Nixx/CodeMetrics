import {Component, Pipe, PipeTransform} from "@angular/core";
import { Router } from "@angular/router";
import {IChapinMetric} from "./ChapinMetric";

@Component({
  selector: 'halsted-app',
  template: `
    <div class="box">
      <i class="nameOfTheTableSpen">Spen</i>
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
            <th>{{retrievedObject2.commonResult}}</th>
          </tr>
          </tfoot>
        </table>
      </div>
      <i class="nameOfTheTable2">Chepin's Full Metric</i>
      <div class="table-scroll2" id="chapin">
        <table>
          <thead>
          <tr>
            <th>Group of variables</th>
            <th>Variables related to the group</th>
            <th>Number of variables in the group</th>
          </tr>
          </thead>
          <tbody>
          <tr>
            <td>P</td>
            <td>{{retrievedObject1.chapinTypes.P}}</td>
            <td>{{retrievedObject1.chapinTypes.P.length}}</td>
          </tr>
          <tr>
            <td>M</td>
            <td>{{retrievedObject1.chapinTypes.M}}</td>
            <td>{{retrievedObject1.chapinTypes.M.length}}</td>
          </tr>
          <tr>
            <td>C</td>
            <td>{{retrievedObject1.chapinTypes.C}}</td>
            <td>{{retrievedObject1.chapinTypes.C.length}}</td>
          </tr>
          <tr>
            <td>T</td>
            <td>{{retrievedObject1.chapinTypes.T}}</td>
            <td>{{retrievedObject1.chapinTypes.T.length}}</td>
          </tr>
          <th>Chepin's metric</th>
          <th>Q = 1*{{retrievedObject1.chapinTypes.P.length}} + 2*{{retrievedObject1.chapinTypes.M.length}}
              + 3*{{retrievedObject1.chapinTypes.C.length}} + 0.5*{{retrievedObject1.chapinTypes.T.length}} = {{retrievedObject1.metricResult}}
          </th>
          <th></th>
          </tbody>
        </table>
      </div>
      <i class="nameOfTheTable2" id="nameChapinIO">Chepin's input/output metric</i>
      <div class="table-scroll2" id="chapinIO">
        <table>
          <thead>
          <tr>
            <th>Group of variables</th>
            <th>Variables related to the group</th>
            <th>Number of variables in the group</th>
          </tr>
          </thead>
          <tbody>
          <tr>
            <td>P</td>
            <td>{{retrievedObject1.chapinIOTypes.P}}</td>
            <td>{{retrievedObject1.chapinIOTypes.P.length}}</td>
          </tr>
          <tr>
            <td>M</td>
            <td>{{retrievedObject1.chapinIOTypes.M}}</td>
            <td>{{retrievedObject1.chapinIOTypes.M.length}}</td>
          </tr>
          <tr>
            <td>C</td>
            <td>{{retrievedObject1.chapinIOTypes.C}}</td>
            <td>{{retrievedObject1.chapinIOTypes.C.length}}</td>
          </tr>
          <tr>
            <td>T</td>
            <td>{{retrievedObject1.chapinIOTypes.T}}</td>
            <td>{{retrievedObject1.chapinIOTypes.T.length}}</td>
          </tr>
          </tbody>
          <tfoot>
          <tr>
            <th>Chepin's metric</th>
            <th>Q = 1*{{retrievedObject1.chapinIOTypes.P.length}} + 2*{{retrievedObject1.chapinIOTypes.M.length}}
              + 3*{{retrievedObject1.chapinIOTypes.C.length}} + 0.5*{{retrievedObject1.chapinIOTypes.T.length}} = {{retrievedObject1.metricIOResult}}
            </th>
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
  retrievedObject1: IChapinMetric = JSON.parse(<string>localStorage.getItem('key1'));
  retrievedObject2 = JSON.parse(<string>localStorage.getItem('key2'));

  // Returning to the page with the input of the code for calculating the Halstead metric
  constructor(private router: Router){}
  goBack(){
    this.router.navigate(['']);
  }

}
