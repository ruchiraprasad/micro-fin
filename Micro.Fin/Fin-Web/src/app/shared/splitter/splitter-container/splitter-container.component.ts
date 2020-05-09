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
  
  shadesEl: any;
  constructor(private splitterService : SplitterService) { }

  ngOnInit() {
    this.splitterService.initializePanel(this.panelLeftContainerRef, this.panelRightContainerRef);
    this.shadesEl = document.querySelector('.right-panel');
  }
  
  toggleRightPanel(event){
    if (!event.target.classList.contains('expand-btn')) {
      event.target.classList.remove('collapse-btn');
      event.target.classList.add('expand-btn');
    }
    else{
      event.target.classList.remove('expand-btn');
      event.target.classList.add('collapse-btn');
    }
    
    if (!this.shadesEl.classList.contains('expand')) {
      this.shadesEl.classList.remove('collapse');
      this.shadesEl.classList.add('expand');
    }
    else{
      this.shadesEl.classList.remove('expand');
      this.shadesEl.classList.add('collapse');
    }
  }
}
