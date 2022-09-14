import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IChat} from "../models/chat";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  constructor(
    private http: HttpClient
  ) {
  }
  getChats(userId: number):Observable<IChat[]>{
    return this.http.get<IChat[]>(`https://localhost:7117/api/chats/${userId}`);
  }
}
