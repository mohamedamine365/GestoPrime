import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlePointageComponent } from './controle-pointage.component';

describe('ControlePointageComponent', () => {
  let component: ControlePointageComponent;
  let fixture: ComponentFixture<ControlePointageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ControlePointageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ControlePointageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
