import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NastavnikKonsultacijeComponent } from './nastavnik-konsultacije.component';

describe('NastavnikKonsultacijeComponent', () => {
  let component: NastavnikKonsultacijeComponent;
  let fixture: ComponentFixture<NastavnikKonsultacijeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NastavnikKonsultacijeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NastavnikKonsultacijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
