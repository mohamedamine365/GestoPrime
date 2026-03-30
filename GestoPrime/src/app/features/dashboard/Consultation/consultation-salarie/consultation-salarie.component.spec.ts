import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultationSalarieComponent } from './consultation-salarie.component';

describe('ConsultationSalarieComponent', () => {
  let component: ConsultationSalarieComponent;
  let fixture: ComponentFixture<ConsultationSalarieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultationSalarieComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConsultationSalarieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
