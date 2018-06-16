import { Injectable } from '@angular/core';
import { StudentKonsultacije, Razlog } from '../models/student-konsultacije';
import { Observable } from 'rxjs/Observable';
import { Student } from '../models/student';
import { RequestOptions, Request, RequestMethod, ResponseContentType } from '@angular/http';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Search } from '../models/search';
import { DatumKonsultacija } from '../models/datum-konsultacija';
import * as moment from 'moment';
import { ZakazaneKonsultacijeRequest } from '../models/zakazane-konsultacije-reguest';
import { ZakazaneKonsultacijeResponse } from '../models/zakazane-konsultacije-response';

@Injectable()
export class StudentService {

  constructor(public http: HttpClient) { }

  getAllKonsultacijeByStudentId(studentId: number): Observable<StudentKonsultacije[]> {
    return this.http.get<StudentKonsultacije[]>(`http://localhost:63561/student-konsultacije/bystudent/${studentId}`);
  }

  getAllKonsultacijeByNastavnikId(nastavnikId: number): Observable<StudentKonsultacije[]> {
    return this.http.get<StudentKonsultacije[]>(`http://localhost:63561/student-konsultacije/bynastavnik/${nastavnikId}`);
  }

  getAllZakazaneKonsultacijeByNastavnikId(zakazaneKonsultacijeRequest: ZakazaneKonsultacijeRequest): Observable<ZakazaneKonsultacijeResponse[]> {
    return this.http.post<ZakazaneKonsultacijeResponse[]>(`http://localhost:63561/student-konsultacije/zakzanekonsultacijebynastavnik/`, zakazaneKonsultacijeRequest);
  }

  groupKonsultacijeByNastavnik(studentId: number): Observable<StudentKonsultacije[]> {
    return this.http.get<StudentKonsultacije[]>(`http://localhost:63561/student-konsultacije/groupbynastavnik/${studentId}`);
  }

  groupKonsultacijeByDatum(studentId: number): Observable<StudentKonsultacije[]> {
    return this.http.get<StudentKonsultacije[]>(`http://localhost:63561/student-konsultacije/groupbydatum/${studentId}`);
  }

  updateKonsultacije(konsultacija: StudentKonsultacije): Observable<void> {
    return this.http.put<void>(`http://localhost:63561/student-konsultacije`, konsultacija);
  }

  insertKonsultacije(konsultacija: StudentKonsultacije) {
    konsultacija.datumString = moment(konsultacija.datumKonsultacija).format();
    return this.http.post(`http://localhost:63561/student-konsultacije`, konsultacija);
  }

  getAllStudenti(): Observable<Student[]> {
    return this.http.get<Student[]>('http://localhost:63561/studenti');
  }

  searchByNastavnik(searchRequest: Search, studentId: number) {
    return this.http.post<StudentKonsultacije[]>(`http://localhost:63561/student-konsultacije/pretragaponastavniku/${studentId}`, searchRequest);
  }

  getKonsultacija(studentId: number, nastavnikId: number, datumKonsultacija: DatumKonsultacija): Observable<StudentKonsultacije> {
    return this.http.post<StudentKonsultacije>(`http://localhost:63561/student-konsultacije/getkonsultacija/${studentId}/${nastavnikId}`, datumKonsultacija);
  }

  deleteKonsultacija(konsultacija: StudentKonsultacije): Observable<void> {
    return this.http.post<void>(`http://localhost:63561/student-konsultacije/delete`, konsultacija);
  }

  getPdf(searchRequest: Search, studentId: number): Observable<Blob> {
    return this.http.post(`http://localhost:63561/student-konsultacije/generatepdfforstudent/${studentId}`, searchRequest, {
      responseType: 'blob'
    });
  }
}
