import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentKonsultacijeComponent } from './student-konsultacije.component';

describe('StudentKonsultacijeComponent', () => {
  let component: StudentKonsultacijeComponent;
  let fixture: ComponentFixture<StudentKonsultacijeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentKonsultacijeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentKonsultacijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
