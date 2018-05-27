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
import { StudentKonsultacijaComponent } from './components/dodavanje-student-konsultacija/student-konsultacija.component';
import { IzmenaStudentKonsultacijeComponent } from './components/izmena-student-konsultacije/izmena-student-konsultacije.component';
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
import { DodavanjeNastavnikKonsultacijaComponent } from './components/dodavanje-nastavnik-konsultacija/dodavanje-nastavnik-konsultacija.component';
import { IzmenaNastavnikKonsultacijaComponent } from './components/izmena-nastavnik-konsultacija/izmena-nastavnik-konsultacija.component';
import { DayOfWeekPipe } from './pipes/day-of-week.pipe';

const appRoutes: Routes = [
  { path: '', redirectTo: '/prijavljivanje', pathMatch: 'full' },
  { path: 'registracija', component: RegistracijaComponent, canActivate: [AnonymousGuardService] },
  { path: 'prijavljivanje', component: PrijavljivanjeComponent, canActivate: [AnonymousGuardService] },
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent, canActivate: [AuthGuardService] },
  { path: 'dodaj-nastavnik-konsultaciju/:userType', component: DodavanjeNastavnikKonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'dodaj-konsultaciju/:userType', component: StudentKonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'dodaj-konsultaciju/:userType/:konsultacija', component: StudentKonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'izmeni-konsultaciju/:userType/nastavnikId/:nastavnikId/datumKonsultacija/:datumKonsultacija', component: IzmenaStudentKonsultacijeComponent, canActivate: [AuthGuardService] },
  { path: 'izmeni-nastavnik-konsultaciju/:id', component: IzmenaNastavnikKonsultacijaComponent, canActivate: [AuthGuardService] },
  { path: 'nastavnik-konsultacije', component: NastavnikKonsultacijeComponent, canActivate: [AuthGuardService] },
];

@NgModule({
  declarations: [
    // components
    AppComponent,
    NastavnikKonsultacijeComponent,
    StudentKonsultacijeComponent,
    KonsultacijaBoxComponent,
    StudentKonsultacijaComponent,
    RegistracijaComponent,
    PrijavljivanjeComponent,
    IzmenaStudentKonsultacijeComponent,
    DodavanjeNastavnikKonsultacijaComponent,
    IzmenaNastavnikKonsultacijaComponent,
    // pipes
    DateFormatPipe,
    DayOfWeekPipe
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
  providers: [StudentService, NastavnikService, AuthService, AnonymousGuardService, AuthGuardService, UtilService, DayOfWeekPipe, DateFormatPipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})


export class AppModule { }
