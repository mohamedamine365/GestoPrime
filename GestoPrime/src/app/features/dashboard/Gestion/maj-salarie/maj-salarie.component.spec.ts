import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MajSalarieComponent } from './maj-salarie.component';

describe('MajSalarieComponent', () => {
  let component: MajSalarieComponent;
  let fixture: ComponentFixture<MajSalarieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MajSalarieComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MajSalarieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
