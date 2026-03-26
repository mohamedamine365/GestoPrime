import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TauxPrimesComponent } from './taux-primes.component';

describe('TauxPrimesComponent', () => {
  let component: TauxPrimesComponent;
  let fixture: ComponentFixture<TauxPrimesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TauxPrimesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TauxPrimesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
