import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataGeneratorComponent } from './data-generator/data-generator.component';
import { ReportGeneratorComponent } from './report-generator/report-generator.component';

const routes: Routes = [
  {path: "",  component: DataGeneratorComponent, pathMatch: "full"},
  {path: "report",  component: ReportGeneratorComponent, pathMatch: "full"}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
