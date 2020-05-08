import { Component, OnInit, OnDestroy } from '@angular/core';
import { SelectItem } from 'primeng/components/common/selectitem';
import { Message, MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HomeService } from '../home.service';
import { takeUntil, switchMap } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { LoanModel, LoanDetailModel, InterestType } from '../home.model';

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
  isEditMode = false;
 
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
        this.isEditMode = true;
        this.loanFormGroup.get('customerId').disable();
        this.loanFormGroup.get('dateGranted').disable();
        console.log('getSelectedLoanAsObservable');
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

  newLoan(){
    this.loanFormGroup.get('customerId').enable();
    this.loanFormGroup.get('dateGranted').enable();
    this.createLoanDetailForm();
    this.loanDetails = [];
    this.isEditMode = false;
    
    console.log('newLoan', this.loanFormGroup.value);
  }

  onSubmit(){
    if (this.loanFormGroup.valid && this.loanFormGroup.dirty) {
      
      const formValues = Object.assign({}, this.loanFormGroup.value);
      //formValues.id = null;
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
        this.isEditMode = true;
        this.loanFormGroup.get('customerId').disable();
        this.loanFormGroup.get('dateGranted').disable();

        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Loan created successfully', life: 2 });
      });
    }
  }

  onRowEditInit(loanDetail: any) {
    this.clonedLoanDetails[loanDetail.id] = { ...loanDetail };
  }

  onRowEditSave(loanDetail: LoanDetailModel) {
    if (loanDetail.id > 0 && loanDetail.paidDate) {
      loanDetail.paidDate = new Date(loanDetail.paidDate);
      this.homeService.updateLoanDetail(loanDetail)
      .pipe(takeUntil(this.destroy$))
      .subscribe((data : LoanDetailModel) => {
        console.log('onRowEditSave', loanDetail);
        //loanDetail.balance = data.balance;
        this.homeService.getLoanDetails(loanDetail.loanId).subscribe(dd => {
          this.loanDetails = dd;
        })
        delete this.clonedLoanDetails[loanDetail.id];
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Car is updated', life: 2 });
      });
      
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
    console.log('newRow');
    
    const loanDetailModel = new LoanDetailModel();
    loanDetailModel.loanId = this.loanFormGroup.value.id;
    this.homeService.createLoanDetail(loanDetailModel)
    .pipe(takeUntil(this.destroy$))
    .subscribe((data : LoanDetailModel) => {
      //this.loanDetails.push(data);
      this.homeService.getLoanDetails(data.loanId).subscribe(dd => {
        this.loanDetails = dd;
      })
    });
    
  }

  toggleInterestType(loanDetailModel: LoanDetailModel){
    console.log('toggleInterestType', loanDetailModel);
    this.homeService.calculateInterest(loanDetailModel.loanId, loanDetailModel.id, loanDetailModel.isCompoundInterest ? InterestType.CompoundInterest : InterestType.SimpleInterest)
    .pipe(takeUntil(this.destroy$))
    .subscribe(data => {
      console.log('Interest', data);
      this.loanDetails = data;
    })
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

