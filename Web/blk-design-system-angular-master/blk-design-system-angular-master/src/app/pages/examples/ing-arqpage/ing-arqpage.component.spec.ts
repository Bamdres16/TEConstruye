import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IngArqpageComponent } from './ing-arqpage.component';

describe('IngArqpageComponent', () => {
  let component: IngArqpageComponent;
  let fixture: ComponentFixture<IngArqpageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IngArqpageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IngArqpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
