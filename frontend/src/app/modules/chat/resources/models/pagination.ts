import {IMessage} from "./message";

export interface IPagination{
  messages: IMessage[];
  total: number;
}
