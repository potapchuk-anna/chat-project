import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {IChat} from "../models/chat";
import {IPagination} from "../models/pagination";
import * as signalR from '@microsoft/signalr';
import {IMessage} from "../models/message";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private  connection: any = new signalR.HubConnectionBuilder().withUrl("https://localhost:7117/chatsocket")   // mapping to the chathub as in startup.cs
    .configureLogging(signalR.LogLevel.Information)
    .build();
  private recievedChatId?:number;
  private sharedChatId = new Subject<number>();
  constructor(
    private http: HttpClient
  ) {
    this.connection.onclose(async () => {
      await this.start();
    });
    this.connection.on("ReceiveMessageChatId", (chatId: number) => { this.mapReceivedMessage(chatId); });
    this.start();
  }
  public async start() {
    try {
      await this.connection.start();
      console.log("connected");
    } catch (err) {
      console.log(err);
      setTimeout(() => this.start(), 5000);
    }
  }

  private mapReceivedMessage(chatId: number): void {
    this.recievedChatId = chatId;
    this.sharedChatId.next(this.recievedChatId);
  }
  retrieveMappedObject(): Observable<number> {
    return this.sharedChatId.asObservable();
  }
  getMessages(chatId: number, page: number, loginedUserId: number): Observable<IMessage[]> {
    return this.http.get<IMessage[]>(`https://localhost:7117/api/messages/chat/${chatId}/${loginedUserId}?page=${page}`);
  }

  getTotal(chatId: number): Observable<number> {
    return this.http.get<number>(`https://localhost:7117/api/messages/chat/${chatId}/total`);
  }

  post(message: IMessage) {
    this.http.post<void>(`https://localhost:7117/api/messages`,
      {
        text: message.text,
        chatId: message.chatId,
        senderId: message.senderId,
        userId: null,
        replyForId: null
      }).subscribe();
  }

  reply(message: IMessage) {
    this.http.post<void>(`https://localhost:7117/api/messages`,
      {
        text: message.text,
        chatId: message.chatId,
        senderId: message.senderId,
        userId: null,
        replyForId: message.replyFor.id
      }).subscribe();
  }

  privateReply(message: IMessage) {
    this.http.post<void>(`https://localhost:7117/api/messages`,
      {
        text: message.text,
        chatId: null,
        senderId: message.senderId,
        userId: message.replyFor.senderId,
        replyForId: message.replyFor.id
      }).subscribe();
  }
  edit(message: IMessage){
    this.http.put<void>(`https://localhost:7117/api/messages/${message.id}`, {text: message.text})
      .subscribe();
  }
  delete(id: number, isForAll: boolean){
    this.http.delete<void>(`https://localhost:7117/api/messages/${id}?isForAll=${isForAll}`)
      .subscribe();
  }
}
