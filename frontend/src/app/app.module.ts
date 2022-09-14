import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {AuthModule} from "./modules/auth/auth.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {RouterOutlet} from "@angular/router";
import {AuthApiService} from "./modules/auth/resources/auth-api.service";
import {HttpClientModule} from "@angular/common/http";
import {AppRoutingModule} from "./app-routing.module";
import {ChatModule} from "./modules/chat/chat.module";
import {TokenService} from "./modules/auth/resources/token.service";
import {PaginationService} from "./modules/chat/resources/services/pagination.service";
import {MessageService} from "./modules/chat/resources/services/message.service";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AuthModule,
    RouterOutlet,
    AppRoutingModule,
    HttpClientModule,
    ChatModule
  ],
  providers: [
    AuthApiService,
    TokenService,
    MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
