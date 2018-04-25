import { Injectable } from '@angular/core';
import { Nastavnik } from '../models/nastavnik';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Konsultacije } from '../models/konsultacije';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class NastavnikService {

  constructor(private http: HttpClient) { }

  getAllNastavnici(): Observable<Nastavnik[]> {
    return this.http.get<Nastavnik[]>('http://localhost:63561/nastavnici');
  }

  getAllKonsultacijeByNastavnikId(nastavnikId: number): Observable<Konsultacije[]> {
    return this.http.get<Konsultacije[]>(`http://localhost:63561/konsultacije/bynastavnik/${nastavnikId}`);
  }
}
