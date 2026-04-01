import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultPlafondPrimeComponent } from './consult-plafond-prime.component';

describe('ConsultPlafondPrimeComponent', () => {
  let component: ConsultPlafondPrimeComponent;
  let fixture: ComponentFixture<ConsultPlafondPrimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultPlafondPrimeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultPlafondPrimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
