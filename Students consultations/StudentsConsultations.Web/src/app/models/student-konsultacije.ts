import { Student } from './student';
import { Nastavnik } from './nastavnik';
import { DateObject } from 'ngx-bootstrap/chronos/types';
import { Projekat } from './projekat';
import { Ispit } from './ispit';
import { ZavrsniRad } from './zavrsniRad';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

export class StudentKonsultacije {
  studentId: number;
  nastavnikId: number;
  razlogId: number;
  odrzane: boolean;
  datumKonsultacijaZaView: NgbDateStruct;
  datumKonsultacija: Date;
  datumString: string;

  student: Student;
  nastavnik: Nastavnik;
  razlog: Razlog;

  vremeOdString: string;
  vremeOd: Date;
  vremeDo: Date;
  konsultacijaId: number;
}

export class Razlog {
  razlogId: number;
  opis: string;

  projekat: Projekat;
  ispit: Ispit;
  zavrsniRad: ZavrsniRad;

  type: RazlogType;
  nazivTipa: string;
  nazivIspita: string;
  tipZavrsnogRada: string;
}

export enum RazlogType {
  Ispit = 0,

  ZavrsniRad,

  Projekat
}
