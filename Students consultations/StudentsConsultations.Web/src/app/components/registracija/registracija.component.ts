import { Component, OnInit } from '@angular/core';
import { RegistracijaStudenta, RegistracijaNastavnika } from '../../models/registracija';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.scss']
})
export class RegistracijaComponent implements OnInit {

  prikaziRegFormuStudent: boolean;
  prikaziRegFormuNastavnik: boolean;
  registracijaStudenta: RegistracijaStudenta;
  registracijaNastavnika: RegistracijaNastavnika;

  constructor(private authService: AuthService,
    private toastService: ToastrService,
    private router: Router) {
    this.registracijaStudenta = new RegistracijaStudenta();
    this.registracijaNastavnika = new RegistracijaNastavnika();
  }

  ngOnInit() {
  }

  izaberiRegKaoStudent() {
    this.prikaziRegFormuStudent = !this.prikaziRegFormuStudent;
  }

  izaberiRegKaoNastavnik() {
    this.prikaziRegFormuNastavnik = !this.prikaziRegFormuNastavnik;
  }

  registrujNastavnika() {
    this.authService.registracijaNastavnika(this.registracijaNastavnika).subscribe(
      data => {
        this.toastService.success('Uspesno ste se registrovali. Mozete se prijaviti.');
        this.router.navigate(['/prijavljivanje']);
      },
      error => {
        this.toastService.error('Niste se uspesno registrovali.');
      }
    );
  }

  registrujStudenta() {
    this.authService.registracijaStudenta(this.registracijaStudenta).subscribe(data => {
      this.router.navigate(['/prijavljivanje']);
      this.toastService.success('Uspesno ste se registrovali. Mozete se prijaviti.');
    },
      error => {
        this.toastService.error('Niste se uspesno registrovali.');
      });
  }
}
