import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {ChatComponent} from "./chat.component";
import {IsUserLoginedGuard} from "../../core/guards/is-user-logined.guard";

const routes: Routes = [
  {path:"chats", component: ChatComponent, canActivate: [IsUserLoginedGuard]}
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ChatRoutingModule { }
