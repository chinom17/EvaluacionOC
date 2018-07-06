import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InputStandarComponent } from './input-standar.component';

describe('InputStandarComponent', () => {
  let component: InputStandarComponent;
  let fixture: ComponentFixture<InputStandarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InputStandarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InputStandarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
