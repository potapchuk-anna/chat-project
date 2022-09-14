import { Injectable } from '@angular/core';
import {catchError, map, Observable, of, tap} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<boolean>{
    let invalidLogin = false;
    return this.http.post<{ token: string }>('https://localhost:7117/api/auth/login', {
      password: password,
      email: email
    }).pipe(
      tap((result)=>{
        console.log(result.token)
        localStorage.setItem("jwt", result.token)
      }),
      map(() => true),
      catchError((err) => {
        console.log(err);
        return of(false);
      })
    );
  }

  isUserAuthorized(): boolean{
    const token = localStorage.getItem("jwt");
    if(token){
      return true;
    }
    return false;
  }

  logout(){
    console.log("here");
    localStorage.removeItem("jwt");
  }
}
