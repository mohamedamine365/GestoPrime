import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndemniteDeplacementComponent } from './indemnite-deplacement.component';

describe('IndemniteDeplacementComponent', () => {
  let component: IndemniteDeplacementComponent;
  let fixture: ComponentFixture<IndemniteDeplacementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IndemniteDeplacementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IndemniteDeplacementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
