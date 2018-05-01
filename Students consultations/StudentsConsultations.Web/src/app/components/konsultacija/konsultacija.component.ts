import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Nastavnik } from '../../models/nastavnik';
import { Konsultacije, Razlog, RazlogType } from '../../models/konsultacije';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentService } from '../../services/student.service';
import { DatePipe } from '@angular/common';
import { DateFormatPipe } from '../../pipes/date.pipe';
import { UserType } from '../../enums/userType.enum';
import { Student } from '../../models/student';
import { Search } from '../../models/search';

@Component({
  selector: 'app-konsultacija',
  templateUrl: './konsultacija.component.html',
  styleUrls: ['./konsultacija.component.scss'],
  providers: [DateFormatPipe]
})
export class KonsultacijaComponent implements OnInit {

  nastavnici: Nastavnik[];
  studenti: Student[];

  razlozi: any[] = [{ id: 0, naziv: 'Ispit' }, { id: 1, naziv: 'Zavrsni rad' }, { id: 2, naziv: 'Projekat' }];

  konsultacija = new Konsultacije();
  razlog = new Razlog();

  isIspit: boolean;
  isZavrsniRad: boolean;
  isProjekat: boolean;
  userType: UserType;
  UserType = UserType;

  constructor(private nastavnikService: NastavnikService,
    private studentService: StudentService,
    private router: Router,
    private toastrService: ToastrService,
    private dateFormatPipe: DateFormatPipe,
    private route: ActivatedRoute
  ) {

    const userType = route.snapshot.params.userType;
    if (userType === '0') {
      this.userType = UserType.Student;
    } else {
      this.userType = UserType.Nastavnik;
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

  save() {
    console.log('Konsultacije is saving...');

    if (this.userType === UserType.Student) {
      this.konsultacija.studentId = 1;
    } else {
      this.konsultacija.nastavnikId = 1;
    }

    this.konsultacija.razlog = this.razlog;
    this.studentService.insertKonsultacije(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija dodata!', 'Uspesno!');

      if (this.userType === UserType.Student) {
        this.router.navigate(['/student-konsultacije']);
      } else {
        this.router.navigate(['/nastavnik-konsultacije']);
      }
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
}
