import { Component, OnInit, Input } from '@angular/core';
import { Konsultacije } from '../../../models/konsultacije';
import { StudentService } from '../../../services/student.service';
import { Router } from '@angular/router';
import { UserType } from '../../../enums/userType.enum';
import * as moment from 'moment';
import { DateFormatPipe } from '../../../pipes/date.pipe';

@Component({
  selector: 'app-student-konsultacija-box',
  templateUrl: './konsultacija-box.component.html',
  styleUrls: ['./konsultacija-box.component.scss'],
  providers: [DateFormatPipe]
})
export class KonsultacijaBoxComponent implements OnInit {

  @Input() konsultacija: Konsultacije;

  constructor(private studentKonsultacijeService: StudentService,
    private router: Router,
    private dateFormatPipe: DateFormatPipe) { }

  ngOnInit() {
    console.log(this.konsultacija);
  }

  odrzane() {
    this.konsultacija.odrzane = true;
    this.studentKonsultacijeService.updateKonsultacije(this.konsultacija).subscribe(() => {
      console.log('Konsultacije izmenjene');
    });
  }

  updateKonsultaciju(konsultacija: Konsultacije) {
    this.router.navigate(['/izmeni-konsultaciju', UserType.Student, 'nastavnikId', konsultacija.nastavnikId, 'datumKonsultacija', this.dateFormatPipe.transform(konsultacija.datumKonsultacija)]);
  }
}
