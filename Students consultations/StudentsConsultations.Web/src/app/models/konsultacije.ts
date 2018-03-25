import { Student } from './student';
import { Nastavnik } from './nastavnik';
import { DateObject } from 'ngx-bootstrap/chronos/types';
import { Projekat } from './projekat';
import { Ispit } from './ispit';
import { ZavrsniRad } from './zavrsniRad';

export class Konsultacije {
  studentId: number;
  nastavnikId: number;
  razlogId: number;
  odrzane: boolean;
  datumKonsultacija: Date;

  student: Student;
  nastavnik: Nastavnik;
  razlog: Razlog;
}

export class Razlog {
  razlogId: number;
  opis: string;
  projekat: Projekat;
  ispit: Ispit;
  zavrsniRad: ZavrsniRad;
}