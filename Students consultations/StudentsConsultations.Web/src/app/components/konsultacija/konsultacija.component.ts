import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Nastavnik } from '../../models/nastavnik';
import { Konsultacije, Razlog, RazlogType } from '../../models/konsultacije';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentService } from '../../services/student.service';
import { DatePipe } from '@angular/common';
import { DateFormatPipe } from '../../pipes/date.pipe';

@Component({
  selector: 'app-konsultacija',
  templateUrl: './konsultacija.component.html',
  styleUrls: ['./konsultacija.component.scss'],
  providers: [DateFormatPipe]
})
export class KonsultacijaComponent implements OnInit {

  nastavnici: Nastavnik[];

  razlozi: any[] = [{ id: 0, naziv: 'Ispit' }, { id: 1, naziv: 'Zavrsni rad' }, { id: 2, naziv: 'Projekat' }];

  konsultacija = new Konsultacije();
  razlog = new Razlog();

  isIspit: boolean;
  isZavrsniRad: boolean;
  isProjekat: boolean;

  constructor(private nastavnikService: NastavnikService,
    private studentService: StudentService,
    private router: Router,
    private toastrService: ToastrService,
    private dateFormatPipe: DateFormatPipe
  ) {
  }

  ngOnInit() {
    this.nastavnikService.getAllNastavnici().subscribe(response => {
      this.nastavnici = response;
    });
  }

  save() {
    console.log('Konsultacije is saving...');
    this.konsultacija.studentId = 1;
    this.konsultacija.razlog = this.razlog;
    this.studentService.insertKonsultacije(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija dodata!', 'Uspesno!');
      this.router.navigate(['/student-konsultacije']);
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

  cancel() {
    this.router.navigate(['/student-konsultacije']);
  }
}
