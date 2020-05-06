import { Injectable } from '@angular/core';
import { BaseApiService } from '@app/shared/api-services/base-api-service';
import { Observable } from 'rxjs';
import { CustomerModel } from './customer.model';

@Injectable()
export class CustomerService {
    
    constructor(private baseApiService: BaseApiService) { }

    getCustomers(): Observable<CustomerModel[]> {
        return this.baseApiService.get<CustomerModel[]>(`api/customer`);
    }

    saveCustomers(customerModel: CustomerModel): Observable<CustomerModel> {
        return this.baseApiService.post(`api/customer`, customerModel);
    }
}
