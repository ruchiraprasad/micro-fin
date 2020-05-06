import { Injectable } from '@angular/core';
import { BaseApiService } from '@app/shared/api-services/base-api-service';
import { Observable, BehaviorSubject } from 'rxjs';
import { LoanModel, CreateLoanModel } from './home.model';

@Injectable()
export class HomeService {

    private loanBehaviorSubject: BehaviorSubject<number> = new BehaviorSubject<number>(undefined);

    constructor(private baseApiService: BaseApiService) { }

    getSelectedLoanAsObservable(): Observable<number> {
        return this.loanBehaviorSubject.asObservable();
    }

    getSelectedLoan(): number {
        return this.loanBehaviorSubject.getValue();
    }

    setSelectedLoan(loanId: number): void {
        this.loanBehaviorSubject.next(loanId);
    }


    searchLoans(skip: number, take: number, searchText: string): Observable<any> {
        let queryString = `skip=${skip}`;
        queryString += `&take=${take}`;
        queryString += `&searchText=${searchText}`;

        return this.baseApiService.get<LoanModel>(`api/loan/search-loans?${queryString}`);
    }

    getCustomers(): Observable<any> {
        return this.baseApiService.get<any>(`api/customer`);
    }

    createLoan(createLoanModel: CreateLoanModel): Observable<any> {
        return this.baseApiService.post(`api/loan/create`, createLoanModel);
    }

    getLoan(id: number): Observable<any> {
        return this.baseApiService.get<any>(`api/loan/${id}`);
    }

    getLoanDetails(loanId: number) : Observable<any> {
        return this.baseApiService.get<any>(`api/loan/loan-details/${loanId}`);
    }
}
