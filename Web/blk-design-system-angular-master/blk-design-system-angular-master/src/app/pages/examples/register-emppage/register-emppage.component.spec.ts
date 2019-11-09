import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterEmppageComponent } from './register-emppage.component';

describe('RegisterEmppageComponent', () => {
  let component: RegisterEmppageComponent;
  let fixture: ComponentFixture<RegisterEmppageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterEmppageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterEmppageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
