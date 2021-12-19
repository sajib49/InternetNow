import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router,
    public _dataGeneratorService: DataGeneratorService) { }

  ngOnInit(): void {
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
      FloatDataPercentage: null,
      NumericDataPercentage: null
    }
    const dataGeneratorModel: DataGeneratorModel= {
      FileInputs: inputData,
      FileSettings: dataSetting
    }
    this._dataGeneratorService.addData(dataGeneratorModel).subscribe(response=>{
      if(response){
        this.numericValue = response.NumericInput;
        this.alphanumericValue = response.AlphanumericInput.trim();
        this.floatValue = response.FloatInput;
      }
    });
  }

  onStopClick() {

  }

  onGenerateReportClick() {
    this.router.navigate(['/report']);
  }

}
