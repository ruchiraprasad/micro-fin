import { Component, OnInit, OnDestroy } from '@angular/core';
import { HomeService } from '../home.service';
import { LoanModel } from '../home.model';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  styleUrls: ['./loan-list.component.scss']
})
export class LoanListComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<boolean>();
  loans: LoanModel[] = [];
  constructor(private homeService: HomeService) { }

  ngOnInit() {
    this.homeService.searchLoans(0, 100, '')
    .pipe(takeUntil(this.destroy$))
    .subscribe(data => {
      if(data){
        this.loans = data;
      }
    })
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

}
