import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KonsultacijaComponent } from './konsultacija.component';

describe('KonsultacijaComponent', () => {
  let component: KonsultacijaComponent;
  let fixture: ComponentFixture<KonsultacijaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KonsultacijaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KonsultacijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
