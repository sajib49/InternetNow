import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "src/app/models/constant.model";
import { DataGeneratorModel } from "src/app/models/data-generator.model";
import { FileInputModel } from "src/app/models/file-input.model";


@Injectable({
    providedIn: "root"
})
export class DataGeneratorService{
    public constructor(public http: HttpClient) {
    }

    addData(data:DataGeneratorModel): Observable<FileInputModel>{
        return this.http.post<FileInputModel>(Constants.API_MOCK_ENDPOINT+'data-generator/save-data', data);               
    }


}
