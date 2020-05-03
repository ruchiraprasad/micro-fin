import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SplitterContainerComponent } from './splitter-container.component';

describe('SplitterContainerComponent', () => {
  let component: SplitterContainerComponent;
  let fixture: ComponentFixture<SplitterContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SplitterContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SplitterContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
