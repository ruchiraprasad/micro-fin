import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@env/environment';
import { LoginModel } from './user.model';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UserService {

    baseUrl = environment.api;
    constructor(private http: HttpClient) { }

    authenticateUser(user: LoginModel) {
        const url = this.baseUrl + `/api/User/authenticate`;
        return this.http.post<LoginModel>(url, user, httpOptions).pipe(
            tap(_ => console.log(`token=`))
        );
    }
}
