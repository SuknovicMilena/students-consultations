import { Component, OnInit, Input } from '@angular/core';
import { Konsultacije } from '../../../models/konsultacije';

@Component({
  selector: 'app-student-konsultacija',
  templateUrl: './konsultacija.component.html',
  styleUrls: ['./konsultacija.component.scss']
})
export class KonsultacijaComponent implements OnInit {

  @Input() konsultacija: Konsultacije;

  constructor() { }

  ngOnInit() {
    console.log(this.konsultacija);
  }

}
