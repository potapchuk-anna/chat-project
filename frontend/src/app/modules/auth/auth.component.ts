import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {AuthApiService} from "./resources/auth-api.service";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  invalidLogin: boolean = false;
  loginForm = this.formGroup.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(private formGroup: FormBuilder,
              private service: AuthApiService,
              private router: Router) {

  }

  ngOnInit(): void {
    if(this.service.isUserAuthorized()){
      this.router.navigate(["/chats"]);
    }
  }


  onSubmit() {
    const validLogin$ = this.service.login(this.loginForm.value.email!,
      this.loginForm.value.password!);
    validLogin$.subscribe((result) => {
      this.invalidLogin=!result;
      if(result){
        this.router.navigate(["/chats"]);
      }
    })
  }


}
