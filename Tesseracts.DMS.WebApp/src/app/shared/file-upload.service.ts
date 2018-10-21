import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { DocumentTag } from '../Model/document-tag';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  documentTags: DocumentTag[] = [];

  constructor(private httpClient:HttpClient) { 
  }

  getAllDocumentTags(){
    this.httpClient.get("http://localhost:49234/api/document/GetAllDocumentTags").
    subscribe(tags => {this.documentTags = tags as DocumentTag[]});
  }

}
