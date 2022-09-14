import {JwtHelperService} from "@auth0/angular-jwt";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  constructor(
    private jwtHelper: JwtHelperService,
  ) {
  }
  getAccessToken(): string {
    const token = this.getAccessTokenOrNull();

    if (!token) {
      throw new Error('No access token found');
    }

    return token;
  }
  getAccessTokenOrNull(): string | null {
    return localStorage.getItem('jwt');
  }
  getAccessTokenData():number {
    const token = this.getAccessToken();
    const tokenData = this.jwtHelper.decodeToken(token);


    if (!tokenData.id) {
      throw new Error('A value is missing in the access token payload');
    }
    return  tokenData.id;
  }
}
