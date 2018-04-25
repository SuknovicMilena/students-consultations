import { Injectable } from '@angular/core';
import { Konsultacije, Razlog } from '../models/konsultacije';
import { Observable } from 'rxjs/Observable';
import { Student } from '../models/student';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StudentService {

  constructor(public http: HttpClient) { }

  getAllKonsultacijeByStudentId(studentId: number): Observable<Konsultacije[]> {
    return this.http.get<Konsultacije[]>(`http://localhost:63561/konsultacije/bystudent/${studentId}`);
  }

  groupKonsultacijeByNastavnik(studentId: number): Observable<Konsultacije[]> {
    return this.http.get<Konsultacije[]>(`http://localhost:63561/konsultacije/groupbynastavnik/${studentId}`);
  }

  groupKonsultacijeByDatum(studentId: number): Observable<Konsultacije[]> {
    return this.http.get<Konsultacije[]>(`http://localhost:63561/konsultacije/groupbydatum/${studentId}`);
  }

  updateKonsultacije(konsultacija: Konsultacije): Observable<void> {
    return this.http.put<void>(`http://localhost:63561/konsultacije`, konsultacija);
  }

  insertKonsultacije(konsultacija: Konsultacije) {
    return this.http.post(`http://localhost:63561/konsultacije`, konsultacija);
  }

  getAllStudenti(): Observable<Student[]> {
    return this.http.get<Student[]>('http://localhost:63561/studenti');
  }
}
