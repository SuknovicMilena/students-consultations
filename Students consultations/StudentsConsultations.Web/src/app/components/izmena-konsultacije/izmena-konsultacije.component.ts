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
import { DatumKonsultacija } from '../../models/datum-konsultacija';

@Component({
  selector: 'app-izmena-konsultacije',
  templateUrl: './izmena-konsultacije.component.html',
  styleUrls: ['./izmena-konsultacije.component.scss'],
  providers: [DateFormatPipe]
})
export class IzmenaKonsultacijeComponent implements OnInit {

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
  nazivRazloga: string;

  isLoading = true;

  constructor(
    private nastavnikService: NastavnikService,
    private studentService: StudentService,
    private router: Router,
    private toastrService: ToastrService,
    private dateFormatPipe: DateFormatPipe,
    private route: ActivatedRoute
  ) {
    const userType = route.snapshot.params.userType;
    const nastavnikId = route.snapshot.params.nastavnikId;
    const datumKonsultacija = route.snapshot.params.datumKonsultacija;

    if (userType === '0') {
      this.userType = UserType.Student;
    } else {
      this.userType = UserType.Nastavnik;
    }

    if (nastavnikId !== undefined && datumKonsultacija !== undefined) {
      const datum = new DatumKonsultacija();
      datum.datumString = datumKonsultacija;
      this.studentService.getKonsultacija(1, nastavnikId, datum).subscribe(response => {

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
    debugger
    if (this.userType === UserType.Student) {
      this.konsultacija.studentId = 1;
    } else {
      this.konsultacija.nastavnikId = 1;
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
