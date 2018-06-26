import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentKonsultacije } from '../../models/student-konsultacije';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';
import * as moment from 'moment';
import { saveAs } from 'file-saver';
import { UtilService } from '../../services/util.service';
import { AuthService } from '../../services/auth.service';
import { NastavnikKonsultacije } from '../../models/nastavnik-konsultacije';
import { DayOfWeekPipe } from '../../pipes/day-of-week.pipe';
import { TimeFormatPipe } from '../../pipes/time-format.pipe';
import { Student } from '../../models/student';
import { StudentService } from '../../services/student.service';

@Component({
  selector: 'app-nastavnik-konsultacije',
  templateUrl: './nastavnik-konsultacije.component.html',
  styleUrls: ['./nastavnik-konsultacije.component.scss'],
  providers: [TimeFormatPipe]
})
export class NastavnikKonsultacijeComponent implements OnInit {

  nastavnikTerminiKonsultacija: Array<NastavnikKonsultacije>;
  konsultacijeKojeNisuOdrzaneINisuIstekle: Array<StudentKonsultacije> = new Array<StudentKonsultacije>();
  istekleKonsultacijeKojeSuOdrzane: Array<StudentKonsultacije> = new Array<StudentKonsultacije>();
  istekleKonsultacijeKojeNisuOdrzane: Array<StudentKonsultacije> = new Array<StudentKonsultacije>();
  konsultacijeZaStudente: Array<StudentKonsultacije>;

  constructor(private nastavnikService: NastavnikService,
    private utilService: UtilService,
    private studentService: StudentService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.nastavnikService.getAllKonsultacijeByNastavnikId(this.utilService.getNastavnikId()).subscribe(response => {
      this.nastavnikTerminiKonsultacija = response;
      console.log(this.nastavnikTerminiKonsultacija);
      // narandzaste
    });
    this.studentService.getAllKonsultacijeByNastavnikId(this.utilService.getNastavnikId()).subscribe((response) => {
      this.konsultacijeZaStudente = response;
      console.log(this.konsultacijeZaStudente);
      this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) > new Date() && !x.odrzane);
      console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
      this.istekleKonsultacijeKojeSuOdrzane = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) < new Date() && x.odrzane);
      console.log(this.istekleKonsultacijeKojeSuOdrzane);
      this.istekleKonsultacijeKojeNisuOdrzane = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) < new Date() && !x.odrzane);
      console.log(this.istekleKonsultacijeKojeSuOdrzane);
    });
  }

  addKonsultacija() {
    this.router.navigate(['/dodaj-nastavnik-konsultaciju', UserType.Nastavnik]);
  }

  pretrazi(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.nastavnikService.searchByStudentAndDate(search, this.utilService.getNastavnikId()).subscribe(response => {
      this.konsultacijeZaStudente = response;
      this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) > new Date() && !x.odrzane);
      console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
      this.istekleKonsultacijeKojeSuOdrzane = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) < new Date() && x.odrzane);
      console.log(this.istekleKonsultacijeKojeSuOdrzane);
      this.istekleKonsultacijeKojeNisuOdrzane = this.konsultacijeZaStudente.filter(x => new Date(x.vremeOd) < new Date() && !x.odrzane);
      console.log(this.istekleKonsultacijeKojeSuOdrzane);
    });
  }

  updateKonsultaciju(id: number) {
     this.router.navigate(['/izmeni-nastavnik-konsultaciju', id]);
  }

  konsultacijeToPDF(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.nastavnikService.getPdf(search, this.utilService.getNastavnikId()).subscribe((response) => {
      saveAs(response, 'konsultacije.pdf');
    });
  }

  logout() {
    this.authService.logout();
  }
}
