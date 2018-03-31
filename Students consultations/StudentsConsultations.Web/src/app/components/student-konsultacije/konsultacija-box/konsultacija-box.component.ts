import { Component, OnInit, Input } from '@angular/core';
import { Konsultacije } from '../../../models/konsultacije';
import { StudentService } from '../../../services/student.service';

@Component({
  selector: 'app-student-konsultacija-box',
  templateUrl: './konsultacija-box.component.html',
  styleUrls: ['./konsultacija-box.component.scss']
})
export class KonsultacijaBoxComponent implements OnInit {

  @Input() konsultacija: Konsultacije;

  constructor(private studentKonsultacijeService: StudentService) { }

  ngOnInit() {
    console.log(this.konsultacija);
  }

  odrzane() {
    this.konsultacija.odrzane = true;
    this.studentKonsultacijeService.updateKonsultacije(this.konsultacija).subscribe(() => {
      console.log('Konsultacije izmenjene');
    });
  }

}
