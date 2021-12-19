import { Component, OnInit } from '@angular/core';
import { DataGeneratorModel } from '../models/data-generator.model';
import { FileInputModel } from '../models/file-input.model';
import { FileSettingsmodel } from '../models/file-settings.model';
import { RandomTextOrNumberGenerator } from '../shared/models/random-text-number-generator';
import { DataGeneratorService } from '../shared/services/data-generator-service';

@Component({
  selector: 'app-data-generator',
  templateUrl: './data-generator.component.html',
  styleUrls: ['./data-generator.component.css']
})
export class DataGeneratorComponent implements OnInit {  
  numericValue: number;
  alphanumericValue: string;
  floatValue: number;
  fileSizeinKb: number;

  constructor(public randomTextOrNumberGenerator: RandomTextOrNumberGenerator,
    public _dataGeneratorService: DataGeneratorService) { }

  ngOnInit(): void {
    this.numericValue = this.randomTextOrNumberGenerator.getRandomInteger(1,100000);
    this.floatValue = this.randomTextOrNumberGenerator.getRandomFloat(1,100000);
    this.alphanumericValue = this.randomTextOrNumberGenerator.getRandomAlphanumericWithSpace(6);
  }

  onStratClick() {
    const inputData: FileInputModel={
      AlphanumericInput:  this.randomTextOrNumberGenerator.getRandomAlphanumericWithSpace(6),
      FloatInput: this.randomTextOrNumberGenerator.getRandomFloat(1,100000),
      NumericInput: this.randomTextOrNumberGenerator.getRandomInteger(1,100000)
    };
    const dataSetting: FileSettingsmodel= {
      FileSizeInKb: this.fileSizeinKb,
      AlphanumericDataPercentage: null,
      FloatDataPercentage: null
    }
    const dataGeneratorModel: DataGeneratorModel= {
      FileInput: inputData,
      FileSettings: dataSetting
    }

    this._dataGeneratorService.addData(dataGeneratorModel).subscribe(ips=>{
      if(ips){
        console.log(ips);
      }
    });
  }

  onStopClick() {

  }

  onGenerateReportClick() {

  }

}
