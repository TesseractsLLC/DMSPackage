import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DocumentTag } from '../Model/DocumentTag';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  
  documentTags: DocumentTag[] = [];

  constructor(private httpClient:HttpClient) { 
  }

  getAllDocumentTags(){
    this.httpClient.get("http://localhost:49234/api/master/GetAllDocumentTags").
    subscribe(tags => {this.documentTags = tags as DocumentTag[]});
  }

  // uploadFile(droppedFile:UploadFile, file: File){

  //   // console.log(file);
  //   // console.log(droppedFile);
          
  //     // You could upload it like this:
  //     const formData = new FormData();
  //     formData.append('file', file, file.name);

  //     // Headers
  //     const headers = new HttpHeaders({
  //       'security-token': 'mytoken'
  //     })

  //     this.httpClient.post('"http://localhost:49234/api/master/UploadFile', formData, 
  //     //{ headers: headers, responseType: 'blob' }
  //     ).subscribe(data => {
  //       // Sanitized logo returned from backend
  //       console.log(data);
  //     }) 
  // }

}
