import { Injectable } from '@angular/core';
import { Nastavnik } from '../models/nastavnik';
import { Observable } from 'rxjs/Observable';
import { Konsultacije } from '../models/konsultacije';
import { HttpClient } from '@angular/common/http';
import { Search } from '../models/search';
import { DatumKonsultacija } from '../models/datum-konsultacija';

@Injectable()
export class NastavnikService {

  constructor(private http: HttpClient) { }

  getAllNastavnici(): Observable<Nastavnik[]> {
    return this.http.get<Nastavnik[]>('http://localhost:63561/nastavnici');
  }

  getAllKonsultacijeByNastavnikId(nastavnikId: number): Observable<Konsultacije[]> {
    return this.http.get<Konsultacije[]>(`http://localhost:63561/konsultacije/bynastavnik/${nastavnikId}`);
  }

  searchByStudent(searchRequest: Search, nastavnikId: number) {
    return this.http.post<Konsultacije[]>(`http://localhost:63561/konsultacije/pretragapostudentu/${nastavnikId}`, searchRequest);
  }

  deleteKonsultacija(konsultacija: Konsultacije): Observable<void> {
    return this.http.post<void>(`http://localhost:63561/konsultacije/delete`, konsultacija);
  }

  getKonsultacija(studentId: number, nastavnikId: number, datumKonsultacija: DatumKonsultacija): Observable<Konsultacije> {
    return this.http.post<Konsultacije>(`http://localhost:63561/konsultacije/getkonsultacija/${studentId}/${nastavnikId}`, datumKonsultacija);
  }
}
