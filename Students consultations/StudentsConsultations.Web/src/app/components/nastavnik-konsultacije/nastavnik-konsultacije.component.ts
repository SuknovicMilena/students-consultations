import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { Konsultacije } from '../../models/konsultacije';

@Component({
  selector: 'app-nastavnik-konsultacije',
  templateUrl: './nastavnik-konsultacije.component.html',
  styleUrls: ['./nastavnik-konsultacije.component.scss']
})
export class NastavnikKonsultacijeComponent implements OnInit {

  konsultacije: Array<Konsultacije>;

  constructor(private nastavnikService: NastavnikService) { }

  ngOnInit() {
    this.nastavnikService.getAllKonsultacijeByNastavnikId(1).subscribe(response => {
      this.konsultacije = response;
    });
  }

}
