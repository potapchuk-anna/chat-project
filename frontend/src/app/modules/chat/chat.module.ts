import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat.component';
import {ChatRoutingModule} from "./chat-routing.module";
import {ScrollingModule} from "@angular/cdk/scrolling";
import { MessageComponent } from './message/message.component';
import {ReactiveFormsModule} from "@angular/forms";
import {MatMenuModule} from "@angular/material/menu";
import {CdkMenuModule} from "@angular/cdk/menu";



@NgModule({
  declarations: [
    ChatComponent,
    MessageComponent
  ],
  imports: [
    CommonModule,
    ChatRoutingModule,
    ScrollingModule,
    ReactiveFormsModule,
    MatMenuModule,
    CdkMenuModule
  ]
})
export class ChatModule { }
