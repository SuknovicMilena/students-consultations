import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';
import { data } from 'jquery';
import { Options } from 'fullcalendar';
import { Konsultacije } from '../../models/konsultacije';
import { StudentService } from '../../services/student.service';
import { debug } from 'util';

@Component({
  selector: 'app-student-konsultacije',
  templateUrl: './student-konsultacije.component.html',
  styleUrls: ['./student-konsultacije.component.scss']
})
export class StudentKonsultacijeComponent implements OnInit {

  konsultacijeZaStudenta: Array<Konsultacije>;

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;

  constructor(private studentService: StudentService) { }

  ngOnInit() {
    this.konsultacijeZaStudenta = new Array<Konsultacije>();
    this.studentService.getAllKonsultacijeByStudentId(1).subscribe((response: Array<Konsultacije>) =>
      this.konsultacijeZaStudenta = response
    );
  }

  // groupByNastavnik(nastavnikIme: string) {
  //   this.studentService.groupKonsultacijeByNastavnik(1, 'Sasa').subscribe((response: Array<Konsultacije>) => {
  //     this.konsultacijeZaStudenta = response;
  //   });
  // }

  groupByDatum() {
  }

  groupByRazlog() {
  }
}
