import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@env/environment';
import { LoginModel } from './user.model';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseApiService } from '@app/shared/api-services/base-api-service';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UserService {

    constructor(private baseApiService: BaseApiService) { }

    authenticateUser(user: LoginModel) {
        return this.baseApiService.post<LoginModel>(`api/User/authenticate`, user).pipe(
            tap(_ => console.log(`token=`))
        );
    }

    getUsers(){
        return this.baseApiService.get<any>('api/User');
    }
}
