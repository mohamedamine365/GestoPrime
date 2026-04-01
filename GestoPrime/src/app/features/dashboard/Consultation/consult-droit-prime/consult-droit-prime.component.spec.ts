import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultDroitPrimeComponent } from './consult-droit-prime.component';

describe('ConsultDroitPrimeComponent', () => {
  let component: ConsultDroitPrimeComponent;
  let fixture: ComponentFixture<ConsultDroitPrimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultDroitPrimeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultDroitPrimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
