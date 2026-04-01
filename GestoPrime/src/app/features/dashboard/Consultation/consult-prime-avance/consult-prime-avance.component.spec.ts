import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultPrimeAvanceComponent } from './consult-prime-avance.component';

describe('ConsultPrimeAvanceComponent', () => {
  let component: ConsultPrimeAvanceComponent;
  let fixture: ComponentFixture<ConsultPrimeAvanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultPrimeAvanceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultPrimeAvanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
