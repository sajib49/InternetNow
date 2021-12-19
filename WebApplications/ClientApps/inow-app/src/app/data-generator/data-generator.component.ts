import { Component, OnInit } from '@angular/core';
import { RandomTextOrNumberGenerator } from '../shared/services/random-text-number-generator';

@Component({
  selector: 'app-data-generator',
  templateUrl: './data-generator.component.html',
  styleUrls: ['./data-generator.component.css']
})
export class DataGeneratorComponent implements OnInit {  
  numericValue: number | undefined;
  alphanumericValue: string | undefined;
  floatValue: number | undefined;
  fileSizeinKb: number | undefined;

  constructor(public randomTextOrNumberGenerator: RandomTextOrNumberGenerator) { }

  ngOnInit(): void {
    this.numericValue = this.randomTextOrNumberGenerator.getRandomInteger(1,100000);
    this.floatValue = this.randomTextOrNumberGenerator.getRandomFloat(1,100000);
    this.alphanumericValue = this.randomTextOrNumberGenerator.getRandomAlphanumericWithSpace(6);
  }

  onStratClick() {

  }

  onStopClick() {

  }

  onGenerateReportClick() {

  }

}
