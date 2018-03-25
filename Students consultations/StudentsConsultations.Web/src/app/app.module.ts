import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AlertModule } from 'ngx-bootstrap';
import { StudentService } from './services/student.service';
import { HttpModule } from '@angular/http';
import { FullCalendarModule } from 'ng-fullcalendar';
import { NastavnikKonsultacijeComponent } from './components/nastavnik-konsultacije/nastavnik-konsultacije.component';
import { StudentKonsultacijeComponent } from './components/student-konsultacije/student-konsultacije.component';
import { KonsultacijaComponent } from './components/student-konsultacije/konsultacija/konsultacija.component';

const appRoutes: Routes = [
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent }
];

@NgModule({
  declarations: [
    // components
    AppComponent,
    NastavnikKonsultacijeComponent,
    StudentKonsultacijeComponent,
    KonsultacijaComponent,
    // pipes
  ],
  imports: [
    AlertModule.forRoot(),
    BrowserModule,
    FullCalendarModule,
    HttpModule,
    RouterModule.forRoot(
      appRoutes
    )
  ],
  providers: [StudentService],
  bootstrap: [AppComponent]
})


export class AppModule { }
