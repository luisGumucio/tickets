import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTicketChatComponent } from './admin-ticket-chat.component';

describe('AdminTicketChatComponent', () => {
  let component: AdminTicketChatComponent;
  let fixture: ComponentFixture<AdminTicketChatComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminTicketChatComponent]
    });
    fixture = TestBed.createComponent(AdminTicketChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
