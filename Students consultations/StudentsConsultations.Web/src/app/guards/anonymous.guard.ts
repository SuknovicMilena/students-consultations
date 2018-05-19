import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { CanActivateChild } from '@angular/router/src/interfaces';
import { AuthService } from '../services/auth.service';
import { UtilService } from '../services/util.service';

@Injectable()
export class AnonymousGuardService implements CanActivate {

  constructor(private authService: AuthService, private router: Router,
    private utilService: UtilService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    if (this.authService.hasToken()) {
      if (this.utilService.getStudentId()) {
        this.router.navigate(['/student-konsultacije']);
      } else {
        this.router.navigate(['/nastavnik-konsultacije']);
      }
    }

    return !this.authService.hasToken();
  }
}

