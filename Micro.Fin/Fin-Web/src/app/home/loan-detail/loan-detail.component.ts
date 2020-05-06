import { Component, OnInit, OnDestroy } from '@angular/core';
import { SelectItem } from 'primeng/components/common/selectitem';
import { Message, MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeService } from '../home.service';
import { takeUntil, switchMap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { LoanModel } from '../home.model';

@Component({
  selector: 'app-loan-detail',
  templateUrl: './loan-detail.component.html',
  styleUrls: ['./loan-detail.component.scss']
})
export class LoanDetailComponent implements OnInit, OnDestroy {

  loanFormGroup: FormGroup;
  customers: any[] = []
  destroy$ = new Subject<boolean>();
  loanDetails: any[] = [];
  clonedLoanDetails: { [s: string]: any; } = {};
  
  brands: SelectItem[];
  clonedCars: { [s: string]: Car; } = {};

  bsConfig: Partial<BsDatepickerConfig> = {
    dateInputFormat: 'DD.MM.YYYY', containerClass: 'theme-dark-blue datepicker-position',
    customTodayClass: 'highlight-today', adaptivePosition: true, dateCustomClasses: [{ date: new Date(), classes: ['highlight-today'] }]
  };

  constructor(private formBuilder: FormBuilder, private homeService: HomeService, private messageService: MessageService) {
    this.createLoanDetailForm();
   }

  ngOnInit() {
    this.getCustomers();

    this.homeService.getSelectedLoanAsObservable()
    .pipe(takeUntil(this.destroy$))
    .subscribe(loanId => {
      if(loanId){
        this.homeService.getLoan(loanId)
        .pipe(
          switchMap(loan => {
            console.log('Saved data', loan);
            this.setLoanDetailFormValues(loan);
            return this.homeService.getLoanDetails(loan.id);
          }))
        .pipe(takeUntil(this.destroy$))
        .subscribe(data => {
          console.log('lonaDetails', data);
          this.loanDetails = data;
        });
      }
    });
  }

  createLoanDetailForm() {
    this.loanFormGroup = this.formBuilder.group({
      'id': [null],
      'customerId': [null, Validators.compose([Validators.required])],
      'initialLoanAmount': [null, Validators.compose([Validators.required])],
      'dateGranted': [new Date(), Validators.compose([Validators.required])],
      'periodMonths': [6],
      'interest': [5],
      'security': [''],
      'propertyValue': [null],
      'capitalOutstanding': [null]
    });
  }

  setLoanDetailFormValues(loan: any){
    this.loanFormGroup.setValue({
      id: loan.id,
      customerId: loan.customerId,
      initialLoanAmount: loan.initialLoanAmount,
      dateGranted: new Date(loan.dateGranted),
      periodMonths: loan.periodMonths,
      interest: loan.interest,
      security: loan.security,
      propertyValue: loan.propertyValue,
      capitalOutstanding: loan.capitalOutstanding
    })
  }

  onDateChange(date: Date) {
    
  }

  onSubmit(){
    if (this.loanFormGroup.valid && this.loanFormGroup.dirty) {
      
      const formValues = Object.assign({}, this.loanFormGroup.value);
      formValues.id = null;
      formValues.dateGranted = new Date(formValues.dateGranted);
      formValues.capitalOutstanding = formValues.initialLoanAmount;
      console.log('loanFormGroup', formValues);
      this.homeService.createLoan(formValues)
      .pipe(
        switchMap((loan) => {
          console.log('Saved data', loan);
          return this.homeService.getLoanDetails(loan.id);
        }))
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        console.log('lonaDetails', data);
        this.loanDetails = data;
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Loan created successfully', life: 2 });
      });
    }
  }

  onRowEditInit(loanDetail: any) {
    this.clonedLoanDetails[loanDetail.id] = { ...loanDetail };
  }

  onRowEditSave(loanDetail: any) {
    if (loanDetail.id > 0) {
      delete this.clonedLoanDetails[loanDetail.id];
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Car is updated', life: 2 });
    }
    else {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Year is required' });
    }


  }

  onRowEditCancel(loanDetail: any, index: number) {
    this.loanDetails[index] = this.clonedLoanDetails[loanDetail.id];
    delete this.clonedLoanDetails[loanDetail.id];
  }

  newRow() {
    return { brand: '', color: '', vin: '', year: '' };
  }

  private getCustomers(){
    this.homeService.getCustomers()
    .pipe(takeUntil(this.destroy$))
    .subscribe(data => {
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

export interface Car {
  vin?;
  year?;
  brand?;
  color?;
  price?;
  saleDate?;
}
