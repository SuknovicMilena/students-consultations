import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { CanActivateChild } from '@angular/router/src/interfaces';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | Observable<boolean> | Promise<boolean> {

    if (!this.authService.hasToken()) {
      this.router.navigate(['/prijavljivanje'], {
        queryParams: { returnUrl: state.url }
      });
    }
    return this.authService.hasToken();
  }
}
