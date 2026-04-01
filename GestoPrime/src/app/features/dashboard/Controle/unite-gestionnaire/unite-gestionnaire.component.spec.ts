import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UniteGestionnaireComponent } from './unite-gestionnaire.component';

describe('UniteGestionnaireComponent', () => {
  let component: UniteGestionnaireComponent;
  let fixture: ComponentFixture<UniteGestionnaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UniteGestionnaireComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UniteGestionnaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
