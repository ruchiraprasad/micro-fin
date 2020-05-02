import { environment } from '@env/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

const httpHeaders = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class BaseApiService {
    baseUrl = environment.api_url;

    constructor(private httpClient: HttpClient) { }

    get<T>(endPoint: string): Observable<T> {
        return this.httpClient.get<T>(`${this.baseUrl}/${endPoint}`, httpHeaders);
    }

    post<T>(endPoint: string, body: any): Observable<T> {
        return this.httpClient.post<T>(`${this.baseUrl}/${endPoint}`, body, httpHeaders);

    }

    put<T>(endPoint: string, body: any): Observable<T> {
        return this.httpClient.put<T>(`${this.baseUrl}/${endPoint}`, body, httpHeaders);
    }

    delete<T>(endPoint: string): Observable<T> {
        return this.httpClient.delete<T>(`${this.baseUrl}/${endPoint}`, httpHeaders);
    }

    patch<T>(endPoint: string, body: any): Observable<T> {
        const requestHeaders = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json-patch+json' })
        };
        return this.httpClient.patch<T>(`${this.baseUrl}/${endPoint}`, body, requestHeaders);
    }
}
