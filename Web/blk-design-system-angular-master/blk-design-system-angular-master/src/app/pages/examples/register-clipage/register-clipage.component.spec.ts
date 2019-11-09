import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterClipageComponent } from './register-clipage.component';

describe('RegisterClipageComponent', () => {
  let component: RegisterClipageComponent;
  let fixture: ComponentFixture<RegisterClipageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterClipageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterClipageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
