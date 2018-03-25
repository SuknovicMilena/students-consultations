import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Konsultacije } from '../models/konsultacije';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StudentService {

  constructor(public http: Http) { }

  getAllKonsultacijeByStudentId(studentId: number): Observable<Konsultacije[]> {
    return this.http.get(`http://localhost:63561/konsultacije/${studentId}`).map(response => response.json() as Konsultacije[]);
  }

  groupKonsultacijeByNastavnik(studentId: number, nastavnikIme: string): Observable<Konsultacije[]> {
    return this.http.get(`http://localhost:63561/konsultacije/${studentId}/${nastavnikIme}`)
      .map(response => response.json() as Konsultacije[]);
  }

}
