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
}
