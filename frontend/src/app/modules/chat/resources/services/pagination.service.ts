import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import {BehaviorSubject, Observable, Subscription} from "rxjs";
import {Inject, Injectable, ViewChild} from "@angular/core";
import {PositionStrategy} from "@angular/cdk/overlay";
import {CdkVirtualScrollViewport} from "@angular/cdk/scrolling";
import {MessageService} from "./message.service";
import {IMessage} from "../models/message";

export class PaginationService extends DataSource<IMessage | undefined> {
  private _length = this.total;
  private _pageSize = 20;
  private _cachedData = Array.from<IMessage>({length: this._length});
  private _fetchedPages = new Set<number>();
  private readonly _dataStream = new BehaviorSubject<(IMessage | undefined)[]>(this._cachedData);
  private readonly _subscription = new Subscription();

  constructor(private messageService:MessageService,
              private total: number,
              private chatId: number,
              private userId: number) {
    super();

  }

  connect(collectionViewer: CollectionViewer): Observable<(IMessage | undefined)[]> {
    this._subscription.add(
      collectionViewer.viewChange.subscribe(range => {
        const startPage = this._getPageForIndex(range.start);
        const endPage = this._getPageForIndex(range.end - 1);
        for (let i = startPage; i <= endPage; i++) {
          this._fetchPage(i);
        }
      }),
    );
    return this._dataStream!;
  }

  disconnect(): void {
    this._subscription.unsubscribe();
  }

  private _getPageForIndex(index: number): number {
    return Math.floor(index / this._pageSize);
  }

  private _fetchPage(page: number) {
    if (this._fetchedPages.has(page)) {
      return;
    }
    this._fetchedPages.add(page);
    // Use `setTimeout` to simulate fetching data from server.
    this.messageService.getMessages(this.chatId, page+1, this.userId).subscribe((messages) => {
      this._cachedData!.splice(
        page * this._pageSize,
        this._pageSize,
        ...messages,
      );
      this._dataStream!.next(this._cachedData!);
    });
  }

}
