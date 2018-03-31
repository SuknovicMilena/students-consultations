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
import { KonsultacijaBoxComponent } from './components/student-konsultacije/konsultacija-box/konsultacija-box.component';
import { KonsultacijaComponent } from './components/student-konsultacije/konsultacija/konsultacija.component';
import { FormsModule } from '@angular/forms';
import { NastavnikService } from './services/nastavnik.service';
import { MyDatePickerModule } from 'mydatepicker';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


const appRoutes: Routes = [
  { path: 'student-konsultacije', component: StudentKonsultacijeComponent },
  { path: 'dodaj-konsultaciju', component: KonsultacijaComponent }
];

@NgModule({
  declarations: [
    // components
    AppComponent,
    NastavnikKonsultacijeComponent,
    StudentKonsultacijeComponent,
    KonsultacijaBoxComponent,
    KonsultacijaComponent,
    // pipes
  ],
  imports: [
    AlertModule.forRoot(),
    ToastrModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MyDatePickerModule,
    FullCalendarModule,
    HttpModule,
    RouterModule.forRoot(
      appRoutes
    )
  ],
  providers: [StudentService, NastavnikService],
  bootstrap: [AppComponent]
})


export class AppModule { }
