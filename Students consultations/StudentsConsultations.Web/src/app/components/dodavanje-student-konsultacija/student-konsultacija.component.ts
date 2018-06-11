import { Component, OnInit, ViewContainerRef, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Nastavnik } from '../../models/nastavnik';
import { StudentKonsultacije, Razlog, RazlogType } from '../../models/student-konsultacije';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentService } from '../../services/student.service';
import { DatePipe } from '@angular/common';
import { DateFormatPipe } from '../../pipes/date.pipe';
import { UserType } from '../../enums/userType.enum';
import { Student } from '../../models/student';
import { Search } from '../../models/search';
import { DatumKonsultacija } from '../../models/datum-konsultacija';
import { UtilService } from '../../services/util.service';
import { NastavnikKonsultacije } from '../../models/nastavnik-konsultacije';
import { DayOfWeekPipe } from '../../pipes/day-of-week.pipe';
import { NgbDatepickerConfig, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { ZakazaneKonsultacijeResponse } from '../../models/zakazane-konsultacije-response';
import { ZakazaneKonsultacijeRequest } from '../../models/zakazane-konsultacije-reguest';

@Component({
  selector: 'app-konsultacija',
  templateUrl: './student-konsultacija.component.html',
  styleUrls: ['./student-konsultacija.component.scss'],
  providers: [DayOfWeekPipe, NgbDatepickerConfig]
})
export class StudentKonsultacijaComponent implements OnInit {

  nastavnici: Nastavnik[];
  studenti: Student[];

  razlozi: any[] = [{ id: 0, naziv: 'Ispit' }, { id: 1, naziv: 'Zavrsni rad' }, { id: 2, naziv: 'Projekat' }];

  konsultacija = new StudentKonsultacije();
  terminiKonsultacije = new Array<number>();
  zakazaneKonsultacije = new Array<ZakazaneKonsultacijeResponse>();
  slobodneKonsultacije = new Array<ZakazaneKonsultacijeResponse>();
  sveKonsultacijeNastavnika: NastavnikKonsultacije[];
  razlog = new Razlog();
  errorInSelection: string;

  isIspit: boolean;
  isZavrsniRad: boolean;
  isProjekat: boolean;
  userType: UserType;
  UserType = UserType;
  trenutnoIzabraniDanUNedelji: number;

  @ViewChild('trajanjeSelect') trajanjeSelect: ElementRef;

  daniUNedelji: any[] =
    [
      { id: 1, naziv: 'Ponedeljak' },
      { id: 2, naziv: 'Utorak' },
      { id: 3, naziv: 'Sreda' },
      { id: 4, naziv: 'Cetvrtak' },
      { id: 5, naziv: 'Petak' },
      { id: 6, naziv: 'Subota' },
      { id: 7, naziv: 'Nedelja' }
    ];

  minDate: Date;
  maxDate: Date;
  wholeMonth: Array<Date>;

  constructor(
    private nastavnikService: NastavnikService,
    private studentService: StudentService,
    private router: Router,
    private toastrService: ToastrService,
    private route: ActivatedRoute,
    private utilService: UtilService,
    config: NgbDatepickerConfig
  ) {
    const userType = route.snapshot.params.userType;

    this.minDate = new Date();
    this.maxDate = moment().add(1, 'month').toDate();

    config.minDate = { day: this.minDate.getUTCDate(), month: this.minDate.getUTCMonth() + 1, year: this.minDate.getUTCFullYear() };
    config.maxDate = { day: this.maxDate.getUTCDate(), month: this.maxDate.getUTCMonth() + 1, year: this.maxDate.getUTCFullYear() };
    config.firstDayOfWeek = this.minDate.getUTCDay();

    if (userType === '0') {
      this.userType = UserType.Student;
    } else {
      this.userType = UserType.Nastavnik;
    }
  }

  isDisabled = this.isDisabledStub.bind(this);

  ngOnInit() {
    this.nastavnikService.getAllNastavnici().subscribe(response => {
      this.nastavnici = response;
    });
    this.studentService.getAllStudenti().subscribe(response => {
      this.studenti = response;
    });
  }

  isDisabledStub(date: NgbDateStruct, current: { month: number }): boolean {
    const daysDisabled = this.terminiKonsultacije.filter(d => {
      return d.toString() === moment(`${date.day}/${date.month}/${date.year}`, 'DD/MM/YYYY').format('e');
    });

    return daysDisabled.length === 0;
  }

  save() {
    console.log('Konsultacije is saving...');

    if (this.userType === UserType.Student) {
      this.konsultacija.studentId = this.utilService.getStudentId();
    } else {
      this.konsultacija.nastavnikId = this.utilService.getNastavnikId();
    }
    this.nastavnikService.getKonsultacijaByNastavnikIdAndDanUNedelji(+this.konsultacija.nastavnikId, this.trenutnoIzabraniDanUNedelji).subscribe((response) => {
      this.konsultacija.konsultacijaId = response.id;
      this.konsultacija.razlog = this.razlog;
      this.studentService.insertKonsultacije(this.konsultacija).subscribe(response => {
        this.toastrService.success('Konsultacija dodata!', 'Uspesno!');

        if (this.userType === UserType.Student) {
          this.router.navigate(['/student-konsultacije']);
        } else {
          this.router.navigate(['/nastavnik-konsultacije']);
        }
      });
    });
  }

  onDateSelection(newDate) {
    this.konsultacija.datumKonsultacijaZaView = newDate;

    const zakazaneKonsultacijeRequest = new ZakazaneKonsultacijeRequest();
    zakazaneKonsultacijeRequest.nastavnikId = this.konsultacija.nastavnikId;
    zakazaneKonsultacijeRequest.zeljeniDatum = new Date(Date.UTC(newDate.year, newDate.month - 1, newDate.day));

    this.trenutnoIzabraniDanUNedelji = moment(zakazaneKonsultacijeRequest.zeljeniDatum).day();
    console.log(this.trenutnoIzabraniDanUNedelji);

    this.studentService.getAllZakazaneKonsultacijeByNastavnikId(zakazaneKonsultacijeRequest).subscribe((konsultacije) => {
      this.zakazaneKonsultacije = konsultacije;
      this.changeTrajanje(null);
    });
  }

  changeTrajanje($event) {
    if (!$event && !this.trajanjeSelect) {
      return;
    }

    const trajanje = $event ? +$event.target.value : this.trajanjeSelect.nativeElement.value;

    if (!trajanje) {
      return;
    }

    const vremeOdSaDatumom = moment(this.konsultacija.vremeOd, 'HH:mm').toDate();
    vremeOdSaDatumom.setDate(this.konsultacija.datumKonsultacijaZaView.day);
    vremeOdSaDatumom.setMonth(this.konsultacija.datumKonsultacijaZaView.month - 1);
    vremeOdSaDatumom.setFullYear(this.konsultacija.datumKonsultacijaZaView.year);

    const potencijalnoVremeOd = moment(vremeOdSaDatumom);
    const potencijalnoVremeDo = potencijalnoVremeOd.clone().add(trajanje, 'minutes');

    const preklapanje = this.zakazaneKonsultacije.filter(k => {
      const vremeOd = moment.utc(k.vremeOd).local();
      const vremeDo = moment.utc(k.vremeDo).local();

      const potencijalnoOdIsBeforeOd = potencijalnoVremeOd.isBefore(vremeOd);

      if (potencijalnoOdIsBeforeOd) {
        const postojiPreklapanje = potencijalnoVremeDo.isAfter(vremeOd);
        return postojiPreklapanje;
      } else {
        const postojiPreklapanje = potencijalnoVremeOd.isBefore(vremeDo);
        return postojiPreklapanje;
      }

    });
    this.konsultacija.datumKonsultacija = vremeOdSaDatumom;
    console.log(preklapanje);

    if (preklapanje.length) {
      this.errorInSelection = 'Vec postoje konsultacije u tom vremenskom okviru';
    } else {
      this.konsultacija.vremeDo = potencijalnoVremeDo.format('HH:mm');
    }
  }

  onVremeOdChange(novoVreme) {
    this.errorInSelection = undefined;
    this.changeTrajanje(null);
  }

  getMinVremeOd() {
    const date = this.konsultacija.datumKonsultacijaZaView;
    const currentSpan = this.sveKonsultacijeNastavnika.filter(k => {
      return k.danUNedelji.toString() === moment(`${date.day}/${date.month}/${date.year}`, 'DD/MM/YYYY').format('e');
    });

    return currentSpan ? moment.utc(currentSpan[0].vremeOd).local().format('HH:mm') : null;
  }

  getMaxVremeOd() {
    const date = this.konsultacija.datumKonsultacijaZaView;
    const currentSpan = this.sveKonsultacijeNastavnika.filter(k => {
      return k.danUNedelji.toString() === moment(`${date.day}/${date.month}/${date.year}`, 'DD/MM/YYYY').format('e');
    });

    return currentSpan ? moment.utc(currentSpan[0].vremeDo).local().format('HH:mm') : null;
  }

  changeRazlog($event) {

    this.isIspit = false;
    this.isProjekat = false;
    this.isZavrsniRad = false;

    this.razlog.type = +$event.target.value;

    switch (this.razlog.type) {
      case 0:
        this.isIspit = true;
        this.razlog.type = RazlogType.Ispit;
        break;
      case 1:
        this.isZavrsniRad = true;
        this.razlog.type = RazlogType.ZavrsniRad;
        break;
      case 2:
        this.isProjekat = true;
        this.razlog.type = RazlogType.Projekat;
        break;
      default:
        break;
    }
  }

  changeTerminKonsultacija($event) {
    this.konsultacija.nastavnikId = $event.target.value;
  }

  cancel() {
    if (this.userType === UserType.Student) {
      this.router.navigate(['/student-konsultacije']);
    } else {
      this.router.navigate(['/nastavnik-konsultacije']);
    }
  }

  changeNastavnik($event) {
    this.konsultacija.nastavnikId = $event.target.value;
    this.nastavnikService.getAllKonsultacijeByNastavnikId(this.konsultacija.nastavnikId).subscribe(response => {
      this.terminiKonsultacije = response.map(x => x.danUNedelji);
      this.sveKonsultacijeNastavnika = response;
    });
  }
}
