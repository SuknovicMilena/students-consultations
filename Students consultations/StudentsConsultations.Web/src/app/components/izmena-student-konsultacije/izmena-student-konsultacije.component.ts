import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Nastavnik } from '../../models/nastavnik';
import { Razlog, RazlogType, StudentKonsultacije } from '../../models/student-konsultacije';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentService } from '../../services/student.service';
import { DatePipe } from '@angular/common';
import { DateFormatPipe } from '../../pipes/date.pipe';
import { UserType } from '../../enums/userType.enum';
import { Student } from '../../models/student';
import { Search } from '../../models/search';
import { DatumKonsultacija } from '../../models/datum-konsultacija';
import { UtilService } from '../../services/util.service';
import { DayOfWeekPipe } from '../../pipes/day-of-week.pipe';

@Component({
  selector: 'app-izmena-konsultacije',
  templateUrl: './izmena-student-konsultacije.component.html',
  styleUrls: ['./izmena-student-konsultacije.component.scss']
})
export class IzmenaStudentKonsultacijeComponent implements OnInit {

  nastavnici: Nastavnik[];
  studenti: Student[];

  razlozi: any[] = [{ id: 0, naziv: 'Ispit' }, { id: 1, naziv: 'Zavrsni rad' }, { id: 2, naziv: 'Projekat' }];

  konsultacija = new StudentKonsultacije();
  razlog = new Razlog();

  isIspit: boolean;
  isZavrsniRad: boolean;
  isProjekat: boolean;
  userType: UserType;
  UserType = UserType;
  nazivRazloga: string;

  isLoading = true;

  currentNastavnik: number;
  currentStudent: number;
  datumKonsultacija: string;

  constructor(
    private nastavnikService: NastavnikService,
    private studentService: StudentService,
    private router: Router,
    private toastrService: ToastrService,
    private dateFormatPipe: DateFormatPipe,
    private route: ActivatedRoute,
    private utilService: UtilService
  ) {
    const userType = route.snapshot.params.userType;
    const datumKonsultacija = route.snapshot.params.datumKonsultacija;

    if (userType === '0') {
      this.userType = UserType.Student;
      this.currentNastavnik = route.snapshot.params.nastavnikId;
    } else {
      this.userType = UserType.Nastavnik;
      this.currentStudent = route.snapshot.params.studentId;
    }

    if (this.currentNastavnik !== undefined && datumKonsultacija !== undefined) {
      const datum = new DatumKonsultacija();
      datum.datumString = datumKonsultacija;
      this.studentService.getKonsultacija(this.utilService.getStudentId(), this.currentNastavnik, datum).subscribe(response => {

        this.razlog = response.razlog;
        console.log(this.razlog);
        this.konsultacija = response;
        console.log(this.konsultacija);

        if (this.konsultacija.razlog.ispit != null) {
          this.nazivRazloga = 'Ispit';
          this.isIspit = true;
        }
        if (this.konsultacija.razlog.projekat != null) {
          this.nazivRazloga = 'Projekat';
          this.isProjekat = true;
        }
        if (this.konsultacija.razlog.zavrsniRad != null) {
          this.nazivRazloga = 'Zavrsni rad';
          this.isZavrsniRad = true;
        }
        this.isLoading = false;
      });
    }

    if (this.currentStudent !== undefined && datumKonsultacija !== undefined) {
      // const datum = new DatumKonsultacija();
      // datum.datumString = datumKonsultacija;
      // this.nastavnikService.getKonsultacija(this.currentStudent, this.utilService.getNastavnikId(), datum).subscribe(response => {

      //   this.razlog = response.razlog;
      //   console.log(this.razlog);
      //   this.konsultacija = response;
      //   console.log(this.konsultacija);

      //   if (this.konsultacija.razlog.ispit != null) {
      //     this.nazivRazloga = 'Ispit';
      //     this.isIspit = true;
      //   }
      //   if (this.konsultacija.razlog.projekat != null) {
      //     this.nazivRazloga = 'Projekat';
      //     this.isProjekat = true;
      //   }
      //   if (this.konsultacija.razlog.zavrsniRad != null) {
      //     this.nazivRazloga = 'Zavrsni rad';
      //     this.isZavrsniRad = true;
      //   }
      //   this.isLoading = false;
      // });
    }
  }

  ngOnInit() {
    this.nastavnikService.getAllNastavnici().subscribe(response => {
      this.nastavnici = response;
    });
    this.studentService.getAllStudenti().subscribe(response => {
      this.studenti = response;
    });
  }

  onDateChanged($event) {
    this.konsultacija.datumKonsultacija = new Date(this.dateFormatPipe.transform($event.date));
    console.log(this.konsultacija.datumKonsultacija);
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

  changeNastavnik($event) {
    this.konsultacija.nastavnikId = $event.target.value;
  }

  changeStudent($event) {
    this.konsultacija.studentId = $event.target.value;
  }

  cancel() {
    this.router.navigate(['/student-konsultacije']);
  }

  update() {
    console.log('Konsultacije is updating...');
    if (this.userType === UserType.Student) {
      this.konsultacija.studentId = this.utilService.getStudentId();
    } else {
      this.konsultacija.nastavnikId = this.utilService.getNastavnikId();
    }

    this.studentService.updateKonsultacije(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija izmenjena!', 'Uspesno!');

      if (this.userType === UserType.Student) {
        this.router.navigate(['/student-konsultacije']);
      } else {
        this.router.navigate(['/nastavnik-konsultacije']);
      }
    });
  }

  delete() {
    this.studentService.deleteKonsultacija(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija obrisana!', 'Uspesno!');

      if (this.userType === UserType.Student) {
        this.router.navigate(['/student-konsultacije']);
      } else {
        this.router.navigate(['/nastavnik-konsultacije']);
      }
    });
  }
}
