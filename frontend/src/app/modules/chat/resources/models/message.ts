export interface IMessage {
  id: number;
  text: string;
  senderId: number;
  senderUsername: string;
  replyFor:IMessage;
  chatId: number;
}
