import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private apiUrl = 'https://latestimageviewer-api-djc8gzczfnanfce7.southeastasia-01.azurewebsites.net/Images';

  constructor(private http: HttpClient) {}

  getLatestImage(): Observable<{ url: string; description: string; createdDate: string }> {
    const url = `${this.apiUrl}/latest`;
    return this.http.get<{ url: string; description: string; createdDate: string }>(url);
  }

  getTotalImagesReceived(fromDate: string): Observable<{ totalImages: number; timestamp: string}> {
    const url = `${this.apiUrl}/count`;
    const options = {
      params: {
        receivedFromDate: fromDate,
    } as any,
  };
    return this.http.get<{ totalImages: number; timestamp: string}>(url, options);
  }

}
