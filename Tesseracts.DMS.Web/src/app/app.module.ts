import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule  } from '@angular/material';
import { FileUploadService } from './services/file-upload.service';
import { HttpClientModule } from '@angular/common/http';
import { AngularFileUploaderModule} from "angular-file-uploader";

@NgModule({
  declarations: [
    AppComponent,
    FileUploadComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    HttpClientModule,
    AngularFileUploaderModule,
  ],
  providers: [FileUploadService],
  bootstrap: [AppComponent]
})
export class AppModule { }
