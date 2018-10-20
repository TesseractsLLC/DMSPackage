import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import {
  MatIconModule,
  MatCardModule,
  MatButtonModule,
  MatListModule,
  MatProgressBarModule,
  MatMenuModule
} from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FileUploadModule } from 'ng2-file-upload/ng2-file-upload';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { DashboardComponent } from './dashboard.component';
import { DashboardRoutes } from './dashboard.routing';
import { FormUploadComponent } from '../forms/form-upload/form-upload.component';
import { FileUploadService } from '../shared/file-upload.service';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(DashboardRoutes),
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    MatListModule,
    MatProgressBarModule,
    MatMenuModule,
    ChartsModule,
    NgxDatatableModule,
    FlexLayoutModule,
    FileUploadModule,
  ],
  declarations: [
    DashboardComponent,
    FormUploadComponent
  ],
  providers:[
    FileUploadService
  ]
})
export class DashboardModule {}
