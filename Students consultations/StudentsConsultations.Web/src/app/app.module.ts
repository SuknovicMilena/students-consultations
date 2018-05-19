import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MyDatePickerModule } from 'mydatepicker';
import { FullCalendarModule } from 'ng-fullcalendar';
import { AlertModule } from 'ngx-bootstrap';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { KonsultacijaComponent } from './components/dodavanje-konsultacija/konsultacija.component';
import { IzmenaKonsultacijeComponent } from './components/izmena-konsultacije/izmena-konsultacije.component';
import { NastavnikKonsultacijeComponent } from './components/nastavnik-konsultacije/nastavnik-konsultacije.component';
import { PrijavljivanjeComponent } from './components/prijavljivanje/prijavljivanje.component';
import { RegistracijaComponent } from './components/registracija/registracija.component';
import { KonsultacijaBoxComponent } from './components/student-konsultacije/konsultacija-box/konsultacija-box.component';
import { StudentKonsultacijeComponent } from './components/student-konsultacije/student-konsultacije.component';
import { DateFormatPipe } from './pipes/date.pipe';
import { NastavnikService } from './services/nastavnik.service';
import { StudentService } from './services/student.service';
import { AuthService } from './services/auth.service';
import { AnonymousGuardService } from './guards/anonymous.guard';
import { AuthGuardService } from './guards/auth.guard';
import { UtilService } from './services/util.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';

const appRoutes: Routes = [
  { path: '', redirectTo: '/prijavljivanje', pathMatch: 'full' },
  { path: 'registracija', component: RegistracijaComponent, canActivate: [AnonymousGuardService] },
  { path: 'prijavljivanje', component: PrijavljivanjeComponent, canActivate: [AnonymousGuardService] },
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent, canActivate: [AuthGuardService] },
  { path: 'dodaj-konsultaciju/:userType', component: KonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'dodaj-konsultaciju/:userType/:konsultacija', component: KonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'izmeni-konsultaciju/:userType/nastavnikId/:nastavnikId/datumKonsultacija/:datumKonsultacija', component: IzmenaKonsultacijeComponent, canActivate: [AuthGuardService] },
  { path: 'izmeni-konsultaciju/:userType/studentId/:studentId/datumKonsultacija/:datumKonsultacija', component: IzmenaKonsultacijeComponent, canActivate: [AuthGuardService] },
  { path: 'nastavnik-konsultacije', component: NastavnikKonsultacijeComponent, canActivate: [AuthGuardService] },
];

@NgModule({
  declarations: [
    // components
    AppComponent,
    NastavnikKonsultacijeComponent,
    StudentKonsultacijeComponent,
    KonsultacijaBoxComponent,
    KonsultacijaComponent,
    RegistracijaComponent,
    PrijavljivanjeComponent,
    // pipes
    DateFormatPipe,
    IzmenaKonsultacijeComponent,
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MyDatePickerModule,
    FullCalendarModule,
    HttpClientModule,
    ToastrModule.forRoot({
      newestOnTop: true,
      timeOut: 3000,
      iconClasses: {},
      positionClass: 'toast-bottom-center',
      preventDuplicates: true
    }),
    RouterModule.forRoot(
      appRoutes
    )
  ],
  providers: [StudentService, NastavnikService, AuthService, AnonymousGuardService, AuthGuardService, UtilService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})


export class AppModule { }
