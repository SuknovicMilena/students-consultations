import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { NastavnikKonsultacijeComponent } from './nastavnik-konsultacije/nastavnik-konsultacije.component';
import { StudentKonsultacijeComponent } from './student-konsultacije/student-konsultacije.component';


@NgModule({
  declarations: [
    AppComponent,
    NastavnikKonsultacijeComponent,
    StudentKonsultacijeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
