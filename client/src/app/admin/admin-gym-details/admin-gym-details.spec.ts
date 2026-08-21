import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminGymDetails } from './admin-gym-details';

describe('AdminGymDetails', () => {
  let component: AdminGymDetails;
  let fixture: ComponentFixture<AdminGymDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminGymDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminGymDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
