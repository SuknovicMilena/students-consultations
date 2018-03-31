import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Konsultacije, Razlog } from '../models/konsultacije';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StudentService {

  constructor(public http: Http) { }

  getAllKonsultacijeByStudentId(studentId: number): Observable<Konsultacije[]> {
    return this.http.get(`http://localhost:63561/konsultacije/${studentId}`).map(response => response.json() as Konsultacije[]);
  }

  groupKonsultacijeByNastavnik(studentId: number): Observable<Konsultacije[]> {
    return this.http.get(`http://localhost:63561/konsultacije/groupbynastavnik/${studentId}`)
      .map(response => response.json() as Konsultacije[]);
  }

  groupKonsultacijeByDatum(studentId: number): Observable<Konsultacije[]> {
    return this.http.get(`http://localhost:63561/konsultacije/groupbydatum/${studentId}`)
      .map(response => response.json() as Konsultacije[]);
  }

  updateKonsultacije(konsultacija: Konsultacije): Observable<void> {
    return this.http.put(`http://localhost:63561/konsultacije`, konsultacija).map(response => response.json());
  }

  insertKonsultacije(kosultacija: Konsultacije) {
    return this.http.post(`http://localhost:63561/konsultacije`, kosultacija);
  }
}
