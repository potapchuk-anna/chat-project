import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {PaginationService} from "../resources/services/pagination.service";
import {MessageService} from "../resources/services/message.service";
import {IChat} from "../resources/models/chat";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {IMessage} from "../resources/models/message";
import {MatMenuTrigger} from "@angular/material/menu";
import {MessageInputType} from "../resources/models/message-input-type";

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  @Input() chat?: IChat;
  @Input() userId?: number;
  ds?:PaginationService;
  repliedMessage?: IMessage;
  checkedMessage?:IMessage;
  msInputType: MessageInputType = "regular";
  constructor(private service: MessageService) {
  }
    text = new FormControl('', [Validators.required])

  ngOnInit(): void {

  }

  ngOnChanges():void{
    if(this.chat) {
      this.service.getTotal(this.chat.id).subscribe((total)=>{
        this.ds= new PaginationService(this.service, total, this.chat!.id, this.userId!);
      });
      this.service.retrieveMappedObject().subscribe((chatId)=>{
        if(chatId==this.chat?.id){
          this.service.getTotal(chatId).subscribe((total)=>{
            this.ds= new PaginationService(this.service, total, chatId,this.userId!);
          });
        }
      });
    }

  }
  post(){
    if(!this.text.value){
      return;
    }
    if(this.msInputType=="regular"){
      this.service.post( {
        senderId: this.userId!,
        text: this.text.value!,
        chatId: this.chat!.id
      } as IMessage);
    }
    if (this.msInputType == "reply") {
      this.service.reply({
        senderId: this.userId!,
        text: this.text.value!,
        chatId: this.chat!.id,
        replyFor: this.repliedMessage
      } as IMessage);
    }
    if (this.msInputType == "reply privatly") {
      this.service.privateReply({
        senderId: this.userId!,
        text: this.text.value!,
        replyFor: this.repliedMessage
      } as IMessage);
    }
    if (this.msInputType == "edited"){
      this.service.edit({
        id: this.checkedMessage!.id,
        text: this.text.value
      } as IMessage);
    }
    this.text.reset();
  }

  isLoginedUserMessage(item: IMessage) {
    if(item.senderId==this.userId){
      return true;
    }
    else{
      return false;
    }
  }

  reply(item: IMessage) {
    this.repliedMessage = item;
    console.log(item);
    this.msInputType = "reply";
  }
  replyPrivate(item: IMessage){
    this.repliedMessage=item;
    console.log(item);
    this.msInputType = "reply privatly";
  }

  edit(item: IMessage) {
    this.checkedMessage = item;
    this.msInputType = "edited";
    this.text.setValue(item.text);
  }

  delete(item: IMessage, isForAll: boolean) {
    this.service.delete(item.id, isForAll);
  }
}
