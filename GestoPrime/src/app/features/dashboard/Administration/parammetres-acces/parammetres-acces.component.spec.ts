import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParammetresAccesComponent } from './parammetres-acces.component';

describe('ParammetresAccesComponent', () => {
  let component: ParammetresAccesComponent;
  let fixture: ComponentFixture<ParammetresAccesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParammetresAccesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ParammetresAccesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
