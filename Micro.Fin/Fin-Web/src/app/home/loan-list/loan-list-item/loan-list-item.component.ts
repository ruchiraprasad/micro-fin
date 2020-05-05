import { Component, OnInit, Input } from '@angular/core';
import { LoanModel } from '@app/home/home.model';
import { HomeService } from '@app/home/home.service';

@Component({
  selector: 'app-loan-list-item',
  templateUrl: './loan-list-item.component.html',
  styleUrls: ['./loan-list-item.component.scss']
})
export class LoanListItemComponent implements OnInit {

  @Input() loanModel: LoanModel;

  constructor(private homeService: HomeService) { }

  ngOnInit() {
  }

  loanClicked(loanModel: LoanModel){
    console.log('loanClicked', loanModel);
    this.homeService.setSelectedLoan(loanModel.id);
  }
}
