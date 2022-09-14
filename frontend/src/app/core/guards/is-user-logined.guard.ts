import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import {AuthApiService} from "../../modules/auth/resources/auth-api.service";

@Injectable({
  providedIn: 'root'
})
export class IsUserLoginedGuard implements CanActivate {
  constructor(private authService:AuthApiService) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.authService.isUserAuthorized();
  }

}
