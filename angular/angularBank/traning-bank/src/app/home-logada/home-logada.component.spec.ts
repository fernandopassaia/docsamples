import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeLogadaComponent } from './home-logada.component';

describe('HomeLogadaComponent', () => {
  let component: HomeLogadaComponent;
  let fixture: ComponentFixture<HomeLogadaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeLogadaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeLogadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
