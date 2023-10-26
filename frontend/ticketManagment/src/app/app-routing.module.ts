import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketListComponent } from './ticket-list/ticket-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'ticket-list', pathMatch: "full" },
  { path: 'ticket-list', component: TicketListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
