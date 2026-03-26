import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MajParamComponent } from './maj-param.component';

describe('MajParamComponent', () => {
  let component: MajParamComponent;
  let fixture: ComponentFixture<MajParamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MajParamComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MajParamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
