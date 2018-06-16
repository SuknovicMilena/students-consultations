import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { StudentKonsultacije } from '../../models/student-konsultacije';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';
import * as moment from 'moment';
import { saveAs } from 'file-saver';
import { UtilService } from '../../services/util.service';
import { AuthService } from '../../services/auth.service';
import { NastavnikKonsultacije } from '../../models/nastavnik-konsultacije';
import { DayOfWeekPipe } from '../../pipes/day-of-week.pipe';
import { TimeFormatPipe } from '../../pipes/time-format.pipe';

@Component({
  selector: 'app-nastavnik-konsultacije',
  templateUrl: './nastavnik-konsultacije.component.html',
  styleUrls: ['./nastavnik-konsultacije.component.scss'],
  providers: [TimeFormatPipe]
})
export class NastavnikKonsultacijeComponent implements OnInit {

  nastavnikTerminiKonsultacija: Array<NastavnikKonsultacije>;
  konsultacijeKojeNisuOdrzaneINisuIstekle: Array<NastavnikKonsultacije> = new Array<NastavnikKonsultacije>();

  constructor(private nastavnikService: NastavnikService,
    private utilService: UtilService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.nastavnikService.getAllKonsultacijeByNastavnikId(this.utilService.getNastavnikId()).subscribe(response => {
      this.nastavnikTerminiKonsultacija = response;
      console.log(this.nastavnikTerminiKonsultacija);
      // narandzaste
      // this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacije.filter(x => new Date(x.datumKonsultacija) > new Date() && !x.odrzane);
      // console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
      // this.konsultacije = this.konsultacije.filter(x => new Date(x.datumKonsultacija) <= new Date() || x.odrzane);
      // console.log(this.konsultacije);
    });
  }

  addKonsultacija() {
    this.router.navigate(['/dodaj-nastavnik-konsultaciju', UserType.Nastavnik]);
  }

  pretrazi(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    // this.nastavnikService.searchByStudent(search, this.utilService.getNastavnikId()).subscribe(response => {
    //   this.konsultacije = response;
    //   // narandzaste
    //   this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacije.filter(x => new Date(x.datumKonsultacija) > new Date() && !x.odrzane);
    //   console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
    //   this.konsultacije = this.konsultacije.filter(x => new Date(x.datumKonsultacija) <= new Date() || x.odrzane);
    //   console.log(this.konsultacije);
    // });
  }

  updateKonsultaciju(id: number) {
    this.router.navigate(['/izmeni-nastavnik-konsultaciju', id]);
  }

  konsultacijeToPDF(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.nastavnikService.getPdf(search, this.utilService.getNastavnikId()).subscribe((response) => {
      saveAs(response, 'konsultacije.pdf');
    });
  }

  logout() {
    this.authService.logout();
  }
}
