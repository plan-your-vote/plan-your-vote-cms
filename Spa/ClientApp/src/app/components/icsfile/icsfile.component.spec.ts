import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IcsfileComponent } from './icsfile.component';

describe('IcsfileComponent', () => {
  let component: IcsfileComponent;
  let fixture: ComponentFixture<IcsfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IcsfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IcsfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  // expect(component).toBeTruthy();
  //});
});
