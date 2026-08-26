import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTrainerRequests } from './admin-trainer-requests';

describe('AdminTrainerRequests', () => {
  let component: AdminTrainerRequests;
  let fixture: ComponentFixture<AdminTrainerRequests>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminTrainerRequests],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminTrainerRequests);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
