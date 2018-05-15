import { HttpClientModule } from '@angular/common/http';
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

const appRoutes: Routes = [
  { path: '', redirectTo: '/prijavljivanje', pathMatch: 'full' },
  { path: 'registracija', component: RegistracijaComponent },
  { path: 'prijavljivanje', component: PrijavljivanjeComponent },
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent },
  { path: 'dodaj-konsultaciju/:userType', component: KonsultacijaComponent },
  { path: 'dodaj-konsultaciju/:userType/:konsultacija', component: KonsultacijaComponent },
  { path: 'izmeni-konsultaciju/:userType/nastavnikId/:nastavnikId/datumKonsultacija/:datumKonsultacija', component: IzmenaKonsultacijeComponent },
  { path: 'nastavnik-konsultacije', component: NastavnikKonsultacijeComponent },
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
    ToastrModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MyDatePickerModule,
    FullCalendarModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes
    )
  ],
  providers: [StudentService, NastavnikService],
  bootstrap: [AppComponent]
})


export class AppModule { }
