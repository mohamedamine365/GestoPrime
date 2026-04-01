import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultUniteGestionnaireComponent } from './consult-unite-gestionnaire.component';

describe('ConsultUniteGestionnaireComponent', () => {
  let component: ConsultUniteGestionnaireComponent;
  let fixture: ComponentFixture<ConsultUniteGestionnaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultUniteGestionnaireComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultUniteGestionnaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
