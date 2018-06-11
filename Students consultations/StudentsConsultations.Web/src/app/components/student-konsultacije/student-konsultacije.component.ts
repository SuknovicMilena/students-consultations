import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';
import { data } from 'jquery';
import { Options } from 'fullcalendar';
import { StudentKonsultacije } from '../../models/student-konsultacije';
import { StudentService } from '../../services/student.service';
import { debug } from 'util';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';
import { saveAs } from 'file-saver';
import { UtilService } from '../../services/util.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-student-konsultacije',
  templateUrl: './student-konsultacije.component.html',
  styleUrls: ['./student-konsultacije.component.scss'],
})
export class StudentKonsultacijeComponent implements OnInit {

  konsultacijeZaStudenta: Array<StudentKonsultacije>;

  constructor(private studentService: StudentService,
    private authService: AuthService,
    private utilService: UtilService,
    private router: Router) { }

  ngOnInit() {
    this.konsultacijeZaStudenta = new Array<StudentKonsultacije>();
    this.studentService.getAllKonsultacijeByStudentId(this.utilService.getStudentId()).subscribe((response: Array<StudentKonsultacije>) =>
      this.konsultacijeZaStudenta = response
    );
  }

  groupByNastavnik() {
    this.studentService.groupKonsultacijeByNastavnik(this.utilService.getStudentId()).subscribe((response: Array<StudentKonsultacije>) => {
      this.konsultacijeZaStudenta = response;
      console.log('Group by nastavnik');
    });
  }

  groupByDatum() {
    this.studentService.groupKonsultacijeByDatum(this.utilService.getStudentId()).subscribe((response: Array<StudentKonsultacije>) => {
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
    this.studentService.searchByNastavnik(search, this.utilService.getStudentId()).subscribe(response => {
      this.konsultacijeZaStudenta = response;
    });
  }

  konsultacijeToPDF(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.studentService.getPdf(search, this.utilService.getStudentId()).subscribe((response) => {
      saveAs(response, 'konsultacije.pdf');
    });
  }

  logout() {
    this.authService.logout();
  }
}
