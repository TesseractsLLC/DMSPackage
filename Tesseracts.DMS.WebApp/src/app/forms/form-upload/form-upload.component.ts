import { Component, ChangeDetectionStrategy } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { FileUploadService } from 'src/app/shared/file-upload.service';
import { HttpClient, HttpParams } from '@angular/common/http';

const URL = 'http://localhost:49234/api/document/UploadFile';

@Component({
  selector: 'app-form-upload',
  templateUrl: './form-upload.component.html',
  styleUrls: ['./form-upload.component.scss']
})
export class FormUploadComponent {
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  hasAnotherDropZoneOver: boolean;
  response: string;
  fileUploadService : FileUploadService;

  constructor(private httpClient:HttpClient) {

   // this.fileUploadService.getAllDocumentTags();

    this.uploader = new FileUploader({
      url: URL,
      method:"POST",
      additionalParameter:{
        "DocumentTag" : "1",
        "DocumentTagValue" : "TestCompany"
      },
      //disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
      // formatDataFunctionIsAsync: true,
      // formatDataFunction: async item => {
      //   return new Promise((resolve, reject) => {
      //     resolve({
      //       name: item._file.name,
      //       length: item._file.size,
      //       contentType: item._file.type,
      //       date: new Date()
      //     });
      //   });
      // }
    });

    this.hasBaseDropZoneOver = false;
    this.hasAnotherDropZoneOver = false;

    this.response = '';

    this.uploader.response.subscribe(res => (this.response = res));
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  public fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }

  public downloadTest(fileId:any):void{

    let fileDownloadParams = new HttpParams().set('fileId', fileId);
    console.log("Calling downLoadFile");
    this.httpClient.get("http://localhost:49234/api/document/DownloadFile", 
    {
        params: fileDownloadParams
    }).subscribe(data => this.downLoadFile(data, ''));

    // window.open(window.URL.createObjectURL(data)
    // window.open("http://localhost:49234/api/document/DownloadFile")
  }


  private downLoadFile(data: any, type: string) {
    console.log("In downLoadFile");
    if (data == null || !data){
      console.log("Data is null");
      return;
    }
    console.log(data);
    window.open(window.URL.createObjectURL(data);

    // var blob = new Blob([data], { type: type});
    // var url = window.URL.createObjectURL(blob);
    // var pwa = window.open(url);
    // if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
    //     alert( 'Please disable your Pop-up blocker and try again.');
    // }
}

}
