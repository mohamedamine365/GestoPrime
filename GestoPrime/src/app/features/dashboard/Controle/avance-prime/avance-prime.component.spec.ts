import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvancePrimeComponent } from './avance-prime.component';

describe('AvancePrimeComponent', () => {
  let component: AvancePrimeComponent;
  let fixture: ComponentFixture<AvancePrimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AvancePrimeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AvancePrimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
