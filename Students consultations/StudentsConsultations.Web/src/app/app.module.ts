import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AlertModule } from 'ngx-bootstrap';
import { StudentService } from './services/student.service';
import { FullCalendarModule } from 'ng-fullcalendar';
import { NastavnikKonsultacijeComponent } from './components/nastavnik-konsultacije/nastavnik-konsultacije.component';
import { StudentKonsultacijeComponent } from './components/student-konsultacije/student-konsultacije.component';
import { KonsultacijaBoxComponent } from './components/student-konsultacije/konsultacija-box/konsultacija-box.component';
import { FormsModule } from '@angular/forms';
import { NastavnikService } from './services/nastavnik.service';
import { MyDatePickerModule } from 'mydatepicker';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegistracijaComponent } from './components/registracija/registracija.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { KonsultacijaComponent } from './components/konsultacija/konsultacija.component';
import { DateFormatPipe } from './pipes/date.pipe';
import { HttpClientModule } from '@angular/common/http';

const appRoutes: Routes = [
  { path: '', redirectTo: '/registracija', pathMatch: 'full' },
  { path: 'registracija', component: RegistracijaComponent },
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent },
  { path: 'dodaj-konsultaciju/:userType', component: KonsultacijaComponent },
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
    NavBarComponent,
    // pipes
    DateFormatPipe
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
