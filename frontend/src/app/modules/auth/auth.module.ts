import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthComponent } from './auth.component';
import { MatInputModule } from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {JwtModule} from "@auth0/angular-jwt";
import {environment} from "../../../environments/environment";
import {AuthRoutingModule} from "./auth-routing.module";
@NgModule({
  declarations: [
    AuthComponent
  ],
  exports: [
    AuthComponent
  ],
  imports: [
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    AuthRoutingModule,
    JwtModule.forRoot({
      config:{
        tokenGetter: () => localStorage.getItem("jwt"),
        allowedDomains: [environment.apiHost],
        disallowedRoutes: []
      }
    })
  ]
})
export class AuthModule { }
