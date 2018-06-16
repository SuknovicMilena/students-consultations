import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { StudentKonsultacije } from '../../../models/student-konsultacije';
import { StudentService } from '../../../services/student.service';
import { Router } from '@angular/router';
import { UserType } from '../../../enums/userType.enum';
import * as moment from 'moment';
import { DateFormatPipe } from '../../../pipes/date-format.pipe';

@Component({
  selector: 'app-student-konsultacija-box',
  templateUrl: './konsultacija-box.component.html',
  styleUrls: ['./konsultacija-box.component.scss']
})
export class KonsultacijaBoxComponent implements OnInit {

  @Input() konsultacija: StudentKonsultacije;
  @Output() refreshKonsultacije = new EventEmitter();

  constructor(private studentKonsultacijeService: StudentService,
    private router: Router) { }

  ngOnInit() {
    console.log(this.konsultacija);
  }

  odrzane() {
    this.konsultacija.odrzane = true;
    this.studentKonsultacijeService.updateKonsultacije(this.konsultacija).subscribe(() => {
      console.log('Konsultacije izmenjene');
    });
  }

  updateKonsultaciju(konsultacija: StudentKonsultacije) {
    this.router.navigate(['/izmeni-konsultaciju', UserType.Student, 'nastavnikId', konsultacija.nastavnikId, 'datumKonsultacija', moment.utc(konsultacija.datumKonsultacija).local().format()]);
  }

  obrisi() {
    this.studentKonsultacijeService.deleteKonsultacija(this.konsultacija).subscribe(() => {
      this.refreshKonsultacije.emit();
      console.log('Konsultacije obrisane');
    });
  }
}
