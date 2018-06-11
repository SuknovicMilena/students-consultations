import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UtilService } from '../../services/util.service';
import { ToastrService } from 'ngx-toastr';
import { NastavnikKonsultacije } from '../../models/nastavnik-konsultacije';


@Component({
  selector: 'app-dodavanje-nastavnik-konsultacija',
  templateUrl: './dodavanje-nastavnik-konsultacija.component.html',
  styleUrls: ['./dodavanje-nastavnik-konsultacija.component.scss']
})
export class DodavanjeNastavnikKonsultacijaComponent implements OnInit {

  daniUNedelji: any[] =
    [
      { id: 1, naziv: 'Ponedeljak' },
      { id: 2, naziv: 'Utorak' },
      { id: 3, naziv: 'Sreda' },
      { id: 4, naziv: 'Cetvrtak' },
      { id: 5, naziv: 'Petak' },
      { id: 6, naziv: 'Subota' },
      { id: 7, naziv: 'Nedelja' }
    ];

  konsultacija = new NastavnikKonsultacije();

  constructor(
    private nastavnikService: NastavnikService,
    private router: Router,
    private toastrService: ToastrService,
    private route: ActivatedRoute,
    private utilService: UtilService
  ) { }

  ngOnInit() {
  }

  save() {
    console.log('Konsultacije is saving...');

    this.konsultacija.nastavnikId = this.utilService.getNastavnikId();

    this.nastavnikService.addKonsultacija(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija dodata!', 'Uspesno!');
      this.router.navigate(['/nastavnik-konsultacije']);
    });
  }

  promeniDanUnedelji($event) {
    this.konsultacija.danUNedelji = +$event.target.value;
    console.log(this.konsultacija.danUNedelji);
  }

  cancel() {
    this.router.navigate(['/nastavnik-konsultacije']);
  }
}
