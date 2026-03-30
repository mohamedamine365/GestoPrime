import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlafondPrimeRendementComponent } from './plafond-prime-rendement.component';

describe('PlafondPrimeRendementComponent', () => {
  let component: PlafondPrimeRendementComponent;
  let fixture: ComponentFixture<PlafondPrimeRendementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlafondPrimeRendementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlafondPrimeRendementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
