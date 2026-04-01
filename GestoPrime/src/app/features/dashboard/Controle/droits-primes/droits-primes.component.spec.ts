import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DroitsPrimesComponent } from './droits-primes.component';

describe('DroitsPrimesComponent', () => {
  let component: DroitsPrimesComponent;
  let fixture: ComponentFixture<DroitsPrimesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DroitsPrimesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DroitsPrimesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
