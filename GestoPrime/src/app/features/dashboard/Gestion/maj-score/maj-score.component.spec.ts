import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MajScoreComponent } from './maj-score.component';

describe('MajScoreComponent', () => {
  let component: MajScoreComponent;
  let fixture: ComponentFixture<MajScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MajScoreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MajScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
