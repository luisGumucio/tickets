import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketListComponent } from './ticket-list/ticket-list.component';
import { ChatComponent } from './chat/chat.component';
import { TicketChatComponent } from './ticket-chat/ticket-chat.component';
import { AdminTicketChatComponent } from './admin-ticket-chat/admin-ticket-chat.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: 'ticket-chat', pathMatch: "full" },
  { path: 'ticket-list', component: TicketListComponent},
  { path: 'ticket-chat', component: TicketChatComponent},
  { path: 'admin-ticket-chat', component: AdminTicketChatComponent},
  { path: 'chat', component: ChatComponent },
  { path: 'login', component: LoginComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
