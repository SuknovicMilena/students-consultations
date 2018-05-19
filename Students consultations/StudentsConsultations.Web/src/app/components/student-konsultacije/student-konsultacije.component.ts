import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';
import { data } from 'jquery';
import { Options } from 'fullcalendar';
import { Konsultacije } from '../../models/konsultacije';
import { StudentService } from '../../services/student.service';
import { debug } from 'util';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';

@Component({
  selector: 'app-student-konsultacije',
  templateUrl: './student-konsultacije.component.html',
  styleUrls: ['./student-konsultacije.component.scss']
})
export class StudentKonsultacijeComponent implements OnInit {

  konsultacijeZaStudenta: Array<Konsultacije>;

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;

  constructor(private studentService: StudentService,
    private router: Router) { }

  ngOnInit() {
    this.konsultacijeZaStudenta = new Array<Konsultacije>();
    this.studentService.getAllKonsultacijeByStudentId(1).subscribe((response: Array<Konsultacije>) =>
      this.konsultacijeZaStudenta = response
    );
  }

  groupByNastavnik() {
    this.studentService.groupKonsultacijeByNastavnik(1).subscribe((response: Array<Konsultacije>) => {
      this.konsultacijeZaStudenta = response;
      console.log('Group by nastavnik');
    });
  }

  groupByDatum() {
    this.studentService.groupKonsultacijeByDatum(1).subscribe((response: Array<Konsultacije>) => {
      this.konsultacijeZaStudenta = response;
      console.log('Group by datum');
    });
  }

  groupByRazlog() {
  }

  addKonsultacija() {
    this.router.navigate(['/dodaj-konsultaciju', UserType.Student]);
  }

  pretrazi(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.studentService.searchByNastavnik(search, 1).subscribe(response => {
      this.konsultacijeZaStudenta = response;
    });
  }

  konsultacijeToPDF() {

  }
}
