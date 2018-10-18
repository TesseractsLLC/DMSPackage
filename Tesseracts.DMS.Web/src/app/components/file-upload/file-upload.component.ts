import { Component, OnInit } from '@angular/core';
import { FileUploadService } from 'src/app/services/file-upload.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})

export class FileUploadComponent implements OnInit {

  afuConfig = {
    multiple: true,
    formatsAllowed: ".jpg,.png,.pdf,.docx,.txt,.gif,.jpeg",
    maxSize: "1",
    uploadAPI:  {
      url:"http://localhost:49234/api/master/UploadFile",
      headers: {
      }
    },
    theme: "dragNDrop",
    hideProgressBar: false,
    hideResetBtn: true,
    hideSelectBtn: false
};

  constructor(private fileUploadService:FileUploadService) { }

  ngOnInit() {
     this.fileUploadService.getAllDocumentTags();
  }

  public DocUpload(){
  }
}