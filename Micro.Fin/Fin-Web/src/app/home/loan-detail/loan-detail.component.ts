import { Component, OnInit, OnDestroy } from '@angular/core';
import { SelectItem } from 'primeng/components/common/selectitem';
import { Message, MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeService } from '../home.service';
import { takeUntil, switchMap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';

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
  
  cars2: Car[];
  brands: SelectItem[];
  clonedCars: { [s: string]: Car; } = {};

  cities = [
    {id: 1, name: 'Vilnius'},
    {id: 2, name: 'Kaunas'},
    {id: 3, name: 'Pavilnys', disabled: true},
    {id: 4, name: 'Pabradė'},
    {id: 5, name: 'Klaipėda'}
];

  bsConfig: Partial<BsDatepickerConfig> = {
    dateInputFormat: 'DD.MM.YYYY', containerClass: 'theme-dark-blue datepicker-position',
    customTodayClass: 'highlight-today', adaptivePosition: true, dateCustomClasses: [{ date: new Date(), classes: ['highlight-today'] }]
  };

  constructor(private formBuilder: FormBuilder, private homeService: HomeService, private messageService: MessageService) {
    this.createLoanDetailForm();
   }

  ngOnInit() {
    this.getCustomers();
    this.cars2 = this.getCarsSmall();

    this.brands = [
      { label: 'Audi', value: 'Audi' },
      { label: 'BMW', value: 'BMW' },
      { label: 'Fiat', value: 'Fiat' },
      { label: 'Ford', value: 'Ford' }
    ];

    setTimeout(function () {
      console.log('hide');
      this.msgss = [];
    }, 2000);
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

  onDateChange(date: Date) {
    
  }

  onSubmit(){
    if (this.loanFormGroup.valid && this.loanFormGroup.dirty) {
      
      const formValues = Object.assign({}, this.loanFormGroup.value);
      formValues.id = null;
      formValues.dateGranted = new Date(formValues.dateGranted);
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

  getCarsSmall() {
    return [
      { "brand": "VW", "year": 2012, "color": "Orange", "vin": "dsad231ff" },
      { "brand": "Audi", "year": 2011, "color": "Black", "vin": "gwregre345" },
      { "brand": "Renault", "year": 2005, "color": "Gray", "vin": "h354htr" },
      { "brand": "BMW", "year": 2003, "color": "Blue", "vin": "j6w54qgh" },

    ]
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
