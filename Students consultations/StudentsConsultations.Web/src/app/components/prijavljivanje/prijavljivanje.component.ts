import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-prijavljivanje',
  templateUrl: './prijavljivanje.component.html',
  styleUrls: ['./prijavljivanje.component.scss']
})
export class PrijavljivanjeComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  registrujSe() {
this.router.navigate(['/registracija']);
  }

}
