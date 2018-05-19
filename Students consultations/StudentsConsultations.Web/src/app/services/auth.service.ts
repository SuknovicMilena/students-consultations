import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ObserveOnMessage } from 'rxjs/operators/observeOn';
import { AuthenticationResponse } from '../models/authentication-response';
import { UserType } from '../enums/userType.enum';
import { Observable } from 'rxjs/Observable';
import { RegistracijaStudenta, RegistracijaNastavnika } from '../models/registracija';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient, private router: Router) { }

  authenticate(userType: UserType, korisnickoIme: string, lozinka: string) {
    // const httpOptions = {
    //   headers: new HttpHeaders({
    //     'Content-Type': 'application/x-www-form-urlencoded'
    //   })
    // };
    // const payload = `grant_type=lozinka&korisnickoIme=${korisnickoIme}&korisnickoIme=${lozinka}`;

    let url;
    if (userType === UserType.Student) {
      url = 'http://localhost:63561/studenti/prijavljivanje';
    } else {
      url = 'http://localhost:63561/nastavnici/prijavljivanje';
    }

    return this.http
      .post<AuthenticationResponse>(url, {
        korisnickoIme: korisnickoIme,
        lozinka: lozinka
      })
      .map((response: AuthenticationResponse) => {
        if (response != null && response.token) {
          localStorage.setItem('token', response.token);
          if (response.studentId) {
            localStorage.setItem('studentId', response.studentId.toString());
          } else {
            localStorage.setItem('nastavnikId', response.nastavnikId.toString());
          }
        }
      });
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/prijavljivanje']);
  }

  hasToken(): boolean {
    if (localStorage.getItem('token')) {
      return true;
    }
    return false;
  }

  getToken(): string {
    return localStorage.getItem('token');
  }

  registracijaStudenta(registracija: RegistracijaStudenta) {
    const url = ' http://localhost:63561/studenti/registracija';
    return this.http
      .post(url, registracija)
      .map((response) => { });
  }

  registracijaNastavnika(registracija: RegistracijaNastavnika) {
    const url = ' http://localhost:63561/nastavnici/registracija';
    return this.http
      .post(url, registracija)
      .map((response) => { });
  }
}
