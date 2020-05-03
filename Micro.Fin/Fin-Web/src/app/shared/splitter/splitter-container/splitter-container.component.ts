import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { SplitterService } from '../splitter.service';

@Component({
  selector: 'app-splitter-container',
  templateUrl: './splitter-container.component.html',
  styleUrls: ['./splitter-container.component.scss']
})
export class SplitterContainerComponent implements OnInit {

  @ViewChild('panelleft', { read: ViewContainerRef, static: true }) panelLeftContainerRef;
  @ViewChild('panelright', { read: ViewContainerRef, static: true }) panelRightContainerRef;
  
  constructor(private splitterService : SplitterService) { }

  ngOnInit() {
    this.splitterService.initializePanel(this.panelLeftContainerRef, this.panelRightContainerRef);
  }
  

}
