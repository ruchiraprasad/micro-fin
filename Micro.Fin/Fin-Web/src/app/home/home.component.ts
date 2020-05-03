import { Component, OnInit, AfterViewInit, ComponentFactoryResolver } from '@angular/core';
import { SplitterService } from '@app/shared/splitter/splitter.service';
import { PanelViewMode } from '@app/shared/splitter/splitter.model';
import { LoanListComponent } from './loan-list/loan-list.component';
import { LoanDetailComponent } from './loan-detail/loan-detail.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  constructor(private splitterService: SplitterService, private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit() {
  }

  ngAfterViewInit(){
    this.splitterService.loadLeftPanel(LoanListComponent, PanelViewMode.Collapse, this.componentFactoryResolver);
    this.splitterService.loadRightPanel(LoanDetailComponent, PanelViewMode.Collapse, this.componentFactoryResolver);
    //this.splitterService.closeRightPanel(true);
  }

}
