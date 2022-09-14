import { Component, OnInit } from '@angular/core';
import {AuthApiService} from "../auth/resources/auth-api.service";
import {Router} from "@angular/router";
import {TokenService} from "../auth/resources/token.service";
import {BehaviorSubject, Observable, Subscription} from "rxjs";
import {IChat} from "./resources/models/chat";
import {ChatService} from "./resources/services/chat.service";
import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import {PaginationService} from "./resources/services/pagination.service";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})

export class ChatComponent implements OnInit {

  userId?: number
  chats$?: Observable<IChat[]>;
  selectedChat:IChat;
  constructor(private authService: AuthApiService,
              private chatService: ChatService,
              private token: TokenService,
              private router: Router) {
    this.selectedChat = {} as IChat;
  }

  ngOnInit(): void {

    this.userId = this.token.getAccessTokenData();
    this.chats$ = this.chatService.getChats(this.userId);

  }

  logout() {
    this.authService.logout();
    this.userId = undefined;
    this.router.navigate(["/login"]);
  }
  setSelectedChat(chat: IChat){
    this.selectedChat = chat;
  }
}
