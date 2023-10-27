import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketChatComponent } from './ticket-chat.component';

describe('TicketChatComponent', () => {
  let component: TicketChatComponent;
  let fixture: ComponentFixture<TicketChatComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TicketChatComponent]
    });
    fixture = TestBed.createComponent(TicketChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
