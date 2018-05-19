import { Injectable } from '@angular/core';


@Injectable()
export class UtilService {
  constructor() { }

  getStudentId(): number {
    const studentId = localStorage.getItem('studentId');
    return +studentId;
  }

  getNastavnikId(): number {
     const nastavnikId = localStorage.getItem('nastavnikId');
    return +nastavnikId;
  }
}
