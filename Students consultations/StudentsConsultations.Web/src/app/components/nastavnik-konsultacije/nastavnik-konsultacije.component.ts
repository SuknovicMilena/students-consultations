import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { Konsultacije } from '../../models/konsultacije';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';
import * as moment from 'moment';
import { saveAs } from 'file-saver';
import { UtilService } from '../../services/util.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-nastavnik-konsultacije',
  templateUrl: './nastavnik-konsultacije.component.html',
  styleUrls: ['./nastavnik-konsultacije.component.scss']
})
export class NastavnikKonsultacijeComponent implements OnInit {

  konsultacije: Array<Konsultacije>;
  konsultacijeKojeNisuOdrzaneINisuIstekle: Array<Konsultacije> = new Array<Konsultacije>();

  constructor(private nastavnikService: NastavnikService,
    private utilService: UtilService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.nastavnikService.getAllKonsultacijeByNastavnikId(this.utilService.getNastavnikId()).subscribe(response => {
      this.konsultacije = response;
      console.log(this.konsultacije);
      // narandzaste
      this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacije.filter(x => new Date(x.datumKonsultacija) > new Date() && !x.odrzane);
      console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
      this.konsultacije = this.konsultacije.filter(x => new Date(x.datumKonsultacija) <= new Date());
      console.log(this.konsultacije);
    });
  }

  addKonsultacija() {
    this.router.navigate(['/dodaj-konsultaciju', UserType.Nastavnik]);
  }

  pretrazi(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.nastavnikService.searchByStudent(search, this.utilService.getNastavnikId()).subscribe(response => {
      this.konsultacije = response;
      // narandzaste
      this.konsultacijeKojeNisuOdrzaneINisuIstekle = this.konsultacije.filter(x => new Date(x.datumKonsultacija) > new Date() && !x.odrzane);
      console.log(this.konsultacijeKojeNisuOdrzaneINisuIstekle);
      this.konsultacije = this.konsultacije.filter(x => new Date(x.datumKonsultacija) <= new Date());
      console.log(this.konsultacije);
    });
  }

  updateKonsultaciju(konsultacija: Konsultacije) {
    this.router.navigate(['/izmeni-konsultaciju', UserType.Nastavnik, 'studentId', konsultacija.studentId, 'datumKonsultacija', moment.utc(konsultacija.datumKonsultacija).local().format()]);
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
