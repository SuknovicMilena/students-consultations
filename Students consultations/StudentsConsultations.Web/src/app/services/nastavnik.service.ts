import { Injectable } from '@angular/core';
import { Nastavnik } from '../models/nastavnik';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { Search } from '../models/search';
import { DatumKonsultacija } from '../models/datum-konsultacija';
import { NastavnikKonsultacije } from '../models/nastavnik-konsultacije';

@Injectable()
export class NastavnikService {

  constructor(private http: HttpClient) { }

  getAllNastavnici(): Observable<Nastavnik[]> {
    return this.http.get<Nastavnik[]>('http://localhost:63561/nastavnici');
  }

  getAllKonsultacijeByNastavnikId(nastavnikId: number): Observable<NastavnikKonsultacije[]> {
    return this.http.get<NastavnikKonsultacije[]>(`http://localhost:63561/konsultacije/getallbynastavnik/${nastavnikId}`);
  }

  // searchByStudent(searchRequest: Search, nastavnikId: number) {
  //   return this.http.post<Konsultacije[]>(`http://localhost:63561/konsultacije/pretragapostudentu/${nastavnikId}`, searchRequest);
  // }

  deleteKonsultacija(id: number): Observable<void> {
    return this.http.delete<void>(`http://localhost:63561/konsultacije/${id}`);
  }

  addKonsultacija(konsultacija: NastavnikKonsultacije): Observable<void> {
    return this.http.post<void>(`http://localhost:63561/konsultacije`, konsultacija);
  }

  updateKonsultacija(konsultacija: NastavnikKonsultacije): Observable<void> {
    return this.http.put<void>(`http://localhost:63561/konsultacije`, konsultacija);
  }

  getKonsultacija(id: number): Observable<NastavnikKonsultacije> {
    return this.http.get<NastavnikKonsultacije>(`http://localhost:63561/konsultacije/${id}`);
  }

  getPdf(searchRequest: Search, nastavnikId: number): Observable<Blob> {
    return this.http.post(`http://localhost:63561/konsultacije/generatepdffornastavnik/${nastavnikId}`, searchRequest, {
      responseType: 'blob'
    });
  }
}
