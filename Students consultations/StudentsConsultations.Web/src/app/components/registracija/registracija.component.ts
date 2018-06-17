import { Component, OnInit } from '@angular/core';
import { RegistracijaStudenta, RegistracijaNastavnika } from '../../models/registracija';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

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
    private route: ActivatedRoute,
    private router: Router) {
    this.registracijaStudenta = new RegistracijaStudenta();
    this.registracijaNastavnika = new RegistracijaNastavnika();
  }

  ngOnInit() {
    const userType = this.route.snapshot.params.userType;

    if (userType === '0') {
      this.izaberiRegKaoStudent();
    } else {
      this.izaberiRegKaoNastavnik();
    }
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
