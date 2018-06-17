import { UserType } from '../../enums/userType.enum';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-prijavljivanje',
  templateUrl: './prijavljivanje.component.html',
  styleUrls: ['./prijavljivanje.component.scss']
})
export class PrijavljivanjeComponent implements OnInit {

  prikaziLoginFormu: boolean;
  userType: UserType;
  korisnickoIme: string;
  lozinka: string;

  constructor(private router: Router,
    private authService: AuthService) { }

  ngOnInit() {
  }

  registrujSe() {
    if (this.userType === UserType.Student) {
      this.router.navigate(['/registracija', UserType.Student]);
    } else {
      this.router.navigate(['/registracija', UserType.Nastavnik]);
    }
  }

  prijaviSe() {
    this.authService.authenticate(this.userType, this.korisnickoIme, this.lozinka).subscribe(response => {
      if (this.userType === UserType.Student) {
        this.router.navigate(['/student-konsultacije']);
      } else {
        this.router.navigate(['/nastavnik-konsultacije']);
      }
    });
  }

  prijaviSeKaoStudent() {
    this.userType = UserType.Student;
    this.prikaziLoginFormu = true;
  }

  prijaviSeKaoNastavnik() {
    this.userType = UserType.Nastavnik;
    this.prikaziLoginFormu = true;
  }
}
