import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environments';

import { LoginDto } from '../models/login.dto';
import { RegisterDto } from '../models/register.dto';
import { RefreshTokenDto } from '../models/refresh-token.dto';
import { UpdateUserDto } from '../models/update-user.dto';
import { AuthResponse } from '../models/auth-response.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  private usernameSubject = new BehaviorSubject<string>('');
  username$ = this.usernameSubject.asObservable();

  constructor(private http: HttpClient) {
    const email = localStorage.getItem('localStorage_email');
    if (email) {
      this.isAuthenticatedSubject.next(true);
      this.usernameSubject.next(email);
    }
  }

  setIsAuthenticated(isAuthenticated: boolean, email?: string) {
    if (email) {
      localStorage.setItem('localStorage_email', email);
      this.usernameSubject.next(email);
    } else {
      localStorage.removeItem('localStorage_email');
      this.usernameSubject.next('');
    }
    this.isAuthenticatedSubject.next(isAuthenticated);
  }

  setJwtToken(token: AuthResponse) {
    localStorage.setItem('localStorage_jwt', token.accessToken);
    localStorage.setItem('localStorage_refreshToken', token.refreshToken);
  }

  clearJwtToken() {
    localStorage.removeItem('localStorage_jwt');
    localStorage.removeItem('localStorage_refreshToken');
  }

  login(dto: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(
      `${environment.apiUrl}/ApplicationUsers/LoginUser`,
      dto
    );
  }

  logout(): Observable<void> {
    const email = localStorage.getItem('localStorage_email');
    return this.http.post<void>(
      `${environment.apiUrl}/ApplicationUsers/LogoutUser/${email}`,
      {}
    );
  }

  getByEmail(email: string): Observable<User> {
    return this.http.get<User>(
      `${environment.apiUrl}/ApplicationUsers/GetByEmail/${email}`
    );
  }

  register(dto: RegisterDto): Observable<void> {
    return this.http.post<void>(
      `${environment.apiUrl}/ApplicationUsers/RegisterUser`,
      dto
    );
  }

  getRefreshToken(dto: any) {
    return this.http.post(
      environment.apiUrl + '/ApplicationUsers/RefreshToken',
      dto
    );
  }

  updateUser(dto: UpdateUserDto): Observable<void> {
    return this.http.put<void>(
      `${environment.apiUrl}/ApplicationUsers/UpdateUser`,
      dto
    );
  }
}
