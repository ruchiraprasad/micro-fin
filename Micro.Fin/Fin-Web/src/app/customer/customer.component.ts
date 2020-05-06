import { Component, OnInit, OnDestroy } from '@angular/core';
import { CustomerService } from './customer.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { CustomerModel } from './customer.model';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit, OnDestroy {

  customers: CustomerModel[] = []
  displayDialog: boolean;
  destroy$ = new Subject<boolean>();
  cols: any[];
  selectedCustomer: CustomerModel;
  customer: CustomerModel = {};
  newCustomer: boolean;


  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.getCustomers();

    this.cols = [
      { field: 'id', header: 'Id' },
      { field: 'name', header: 'Name' },
      { field: 'phone', header: 'Phone' },
      { field: 'address', header: 'Address' },
      { field: 'comment', header: 'Comment' }
    ];
  }

  showDialogToAdd() {
    this.newCustomer = true;
    this.customer = {};
    this.displayDialog = true;
  }

  save() {
    let customers = [...this.customers];
    this.customerService.saveCustomers(this.customer)
    .pipe(takeUntil(this.destroy$))
    .subscribe((data: CustomerModel) => {
      console.log('save', data);
      this.customer.id = data.id;

      if (this.newCustomer)
        customers.push(this.customer);
      else
        customers[this.customers.indexOf(this.selectedCustomer)] = this.customer;

      this.customers = customers;
      this.customer = null;
      this.displayDialog = false;
    });

    
  }

  delete() {
    let index = this.customers.indexOf(this.selectedCustomer);
    this.customers = this.customers.filter((val, i) => i != index);
    this.customer = null;
    this.displayDialog = false;
  }

  onRowSelect(event) {
    this.newCustomer = false;
    //this.customer = this.cloneCar(event.data);
    this.customer = Object.assign({}, event.data);
    this.displayDialog = true;
  }

  cloneCar(customerModel: CustomerModel): CustomerModel {
    let car = {};
    for (let prop in customerModel) {
      car[prop] = customerModel[prop];
    }
    return car;
  }

  private getCustomers(){
    this.customerService.getCustomers()
    .pipe(takeUntil(this.destroy$))
    .subscribe((data : CustomerModel[]) => {
      if(data){
        this.customers = data;
      }
    });
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}