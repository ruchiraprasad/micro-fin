import { Injectable } from '@angular/core';
import { BaseApiService } from '@app/shared/api-services/base-api-service';
import { Observable } from 'rxjs';
import { LoanModel, CreateLoanModel } from './home.model';

@Injectable()
export class HomeService {
    constructor(private baseApiService: BaseApiService) { }

    searchLoans(skip: number, take: number, searchText: string): Observable<any> {
        let queryString = `skip=${skip}`;
        queryString += `&take=${take}`;
        queryString += `&searchText=${searchText}`;

        return this.baseApiService.get<LoanModel>(`api/loan/search-loans?${queryString}`);
    }

    getCustomers(): Observable<any> {
        return this.baseApiService.get<LoanModel>(`api/customer`);
    }

    createLoan(createLoanModel: CreateLoanModel): Observable<any> {
        return this.baseApiService.post(`api/loan/create`, createLoanModel);
    }

    getLoanDetails(loanId: number) : Observable<any> {
        return this.baseApiService.get<any>(`api/loan/loan-details/${loanId}`);
    }
}
