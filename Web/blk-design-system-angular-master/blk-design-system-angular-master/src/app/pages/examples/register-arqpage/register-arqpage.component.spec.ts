import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterArqpageComponent } from './register-arqpage.component';

describe('RegisterArqpageComponent', () => {
  let component: RegisterArqpageComponent;
  let fixture: ComponentFixture<RegisterArqpageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterArqpageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterArqpageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
