import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class RandomTextOrNumberGenerator {

    public getRandomInteger(min: number, max: number): number {
        return Math.floor((Math.random() * max) + min);      
    } 

    public getRandomFloat(min: number, max: number): number {
        return Number((min + (max - min) * Math.random()).toFixed(2)) ;      
    }
    
    public getRandomAlphanumericWithSpace(length: number) {
        var result           = '';
        var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        var charactersLength = characters.length;
        for ( var i = 0; i < length; i++ ) {
          result += characters.charAt(Math.floor(Math.random() * charactersLength));
       }
       
       var spaceBefore = '';
       var spaceAfter = '';
       
       for(var j=0;j<Math.floor((Math.random() * 10) + 1);j++) {
           spaceBefore += ' ';
       }
       
       for(var k=0;k<Math.floor((Math.random() * 10) + 1);k++) {
           spaceAfter += ' ';
       }
       
       result = spaceBefore + result + spaceAfter;
       return result;
    }
    
}
