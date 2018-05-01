import { Component, OnInit } from '@angular/core';
import { NastavnikService } from '../../services/nastavnik.service';
import { Konsultacije } from '../../models/konsultacije';
import { Router } from '@angular/router';
import { UserType } from '../../enums/userType.enum';
import { Search } from '../../models/search';

@Component({
  selector: 'app-nastavnik-konsultacije',
  templateUrl: './nastavnik-konsultacije.component.html',
  styleUrls: ['./nastavnik-konsultacije.component.scss']
})
export class NastavnikKonsultacijeComponent implements OnInit {

  konsultacije: Array<Konsultacije>;

  constructor(private nastavnikService: NastavnikService,
    private router: Router) { }

  ngOnInit() {
    this.nastavnikService.getAllKonsultacijeByNastavnikId(1).subscribe(response => {
      this.konsultacije = response;
    });
  }

  addKonsultacija() {
    this.router.navigate(['/dodaj-konsultaciju', UserType.Nastavnik]);
  }

  pretrazi(searchText: string) {
    const search = new Search();
    search.searchText = searchText;
    this.nastavnikService.searchByStudent(search, 1).subscribe(response => {
      this.konsultacije = response;
    });
  }
}
