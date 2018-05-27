import { Component, OnInit } from '@angular/core';
import { NastavnikKonsultacije } from '../../models/nastavnik-konsultacije';
import { NastavnikService } from '../../services/nastavnik.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilService } from '../../services/util.service';
import { ToastrService } from 'ngx-toastr';
import { DayOfWeekPipe } from '../../pipes/day-of-week.pipe';
import { DateFormatPipe } from '../../pipes/date.pipe';

@Component({
  selector: 'app-izmena-nastavnik-konsultacija',
  templateUrl: './izmena-nastavnik-konsultacija.component.html',
  styleUrls: ['./izmena-nastavnik-konsultacija.component.scss']
})
export class IzmenaNastavnikKonsultacijaComponent implements OnInit {

  konsultacija = new NastavnikKonsultacije();

  daniUNedelji: any[] = [{ id: 0, naziv: 'Ponedeljak' },
  { id: 1, naziv: 'Utorak' },
  { id: 2, naziv: 'Sreda' },
  { id: 3, naziv: 'Cetvrtak' },
  { id: 4, naziv: 'Petak' },
  { id: 5, naziv: 'Subota' },
  { id: 6, naziv: 'Nedelja' }];

  constructor(private route: ActivatedRoute, private nastavnikService: NastavnikService, private utilService: UtilService, private router: Router,
    private toastrService: ToastrService, private dateTimePipe: DateFormatPipe) {
    const id = +this.route.snapshot.params.id;
    this.nastavnikService.getKonsultacija(id).subscribe(response => {
      this.konsultacija.nastavnikId = response.nastavnikId;
      this.konsultacija.danUNedelji = response.danUNedelji;
      this.konsultacija.id = response.id;
      this.konsultacija.vremeDo = this.dateTimePipe.transform(response.vremeDo);
      this.konsultacija.vremeOd = this.dateTimePipe.transform(response.vremeOd);
    });
  }

  ngOnInit() {
  }

  update() {
    console.log('Konsultacije is updating...');

    this.nastavnikService.updateKonsultacija(this.konsultacija).subscribe(response => {
      this.toastrService.success('Konsultacija izmenjena!', 'Uspesno!');
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

  delete() {
    this.nastavnikService.deleteKonsultacija(this.konsultacija.id).subscribe(response => {
      this.toastrService.success('Konsultacija obrisana!', 'Uspesno!');
      this.router.navigate(['/nastavnik-konsultacije']);
    });
  }
}
