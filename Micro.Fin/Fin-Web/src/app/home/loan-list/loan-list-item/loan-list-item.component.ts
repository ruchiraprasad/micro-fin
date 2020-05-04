import { Component, OnInit, Input } from '@angular/core';
import { LoanModel } from '@app/home/home.model';

@Component({
  selector: 'app-loan-list-item',
  templateUrl: './loan-list-item.component.html',
  styleUrls: ['./loan-list-item.component.scss']
})
export class LoanListItemComponent implements OnInit {

  @Input() loanModel: LoanModel;

  constructor() { }

  ngOnInit() {
  }

}
