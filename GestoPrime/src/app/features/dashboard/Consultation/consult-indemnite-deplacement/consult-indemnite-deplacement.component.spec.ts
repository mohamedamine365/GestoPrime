import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultIndemniteDeplacementComponent } from './consult-indemnite-deplacement.component';

describe('ConsultIndemniteDeplacementComponent', () => {
  let component: ConsultIndemniteDeplacementComponent;
  let fixture: ComponentFixture<ConsultIndemniteDeplacementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultIndemniteDeplacementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultIndemniteDeplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
