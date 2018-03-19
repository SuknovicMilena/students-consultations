import { Injectable } from '@angular/core';
import { Nastavnik } from '../models/nastavnik';
import { Observable } from 'rxjs/Observable';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class NastavnikService {

  constructor(private http: Http) { }

  getAllNastavnici(): Observable<Array<Nastavnik>> {
    return this.http.get('http://localhost:63561/nastavnici')
      .map(response => response.json() as Array<Nastavnik>);
  }
}
