import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { HomeService } from '../home.service';
import { LoanModel } from '../home.model';
import { takeUntil } from 'rxjs/operators';
import { Subject, Observable, of } from 'rxjs';
import { VirtualScrollerComponent, IPageInfo } from 'ngx-virtual-scroller';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  styleUrls: ['./loan-list.component.scss']
})
export class LoanListComponent implements OnInit, OnDestroy, AfterViewInit  {
  destroy$ = new Subject<boolean>();
  buffer: LoanModel[];
  loading = false;
  isAllLoded = false;
  private previousSkip = -1;

  @ViewChild('scroll', { static: false }) virtualScroller: VirtualScrollerComponent;
  
  constructor(private homeService: HomeService, private ref: ChangeDetectorRef) { 
  }

  ngOnInit() {
    // this.homeService.searchLoans(0, 100, '')
    // .pipe(takeUntil(this.destroy$))
    // .subscribe(data => {
    //   if(data){
    //     this.buffer = data;
    //     this.ref.detectChanges();
    //   }
    // })
    this.buffer= [];
    this.ref.detectChanges();
  }

  ngAfterViewInit(){

  }
  
  fetchMore(event: IPageInfo) {
    console.log('fetchMore', event);
    //const tempBuffer: LoanModel[] = this.buffer;
    if (event.endIndex !== this.buffer.length - 1 || this.isAllLoded) {
      this.loading = false;
      return;
    }
    this.loading = true;
    this.fetchNextChunk(this.buffer.length, 5)
      .pipe(takeUntil(this.destroy$))
      .subscribe((chunk: LoanModel[]) => {
        this.isAllLoded = chunk.length === 0;
        if (chunk.length > 0) {
          chunk.forEach(element => {
            this.buffer.push(element);
          });
        }
        this.loading = false;
      },
        (error: any) => {
          this.loading = false;
        });
  }

  fetchNextChunk(skip: number, limit: number): Observable<LoanModel[]> {
    const emptyList: LoanModel[] = [];
    if (this.previousSkip !== skip) {
      this.previousSkip = skip;
      //const referrerSearchRequest = {skip: skip, take: limit, SearchTexts: this.searchTextList} as ReferrerSearchRequest;
      return this.homeService.searchLoans(skip, limit, '');
    }
      return of(emptyList);
  }

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

}
