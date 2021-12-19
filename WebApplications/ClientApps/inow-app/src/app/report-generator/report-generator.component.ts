import { Component, OnInit } from '@angular/core';
import { DataPercentageModel } from '../models/Data-percentage.model';
import { ReportDataModel } from '../models/report-data.model';
import { DataGeneratorService } from '../shared/services/data-generator-service';

@Component({
  selector: 'app-report-generator',
  templateUrl: './report-generator.component.html',
  styleUrls: ['./report-generator.component.css']
})
export class ReportGeneratorComponent implements OnInit {
  data: ReportDataModel;

  constructor(public _dataGeneratorService: DataGeneratorService,) { }

  ngOnInit(): void {
    this._dataGeneratorService.getData().subscribe(response=>{
      this.data = response;
    });
    
  }

}
