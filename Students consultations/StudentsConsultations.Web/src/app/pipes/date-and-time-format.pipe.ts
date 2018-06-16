import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'dateTimeFormat' })
export class DateTimeFormatPipe implements PipeTransform {

  transform(date: string | Date, format: string = 'DD-MM-YYYY, HH:mm'): string {
    return moment.utc(date).local().format(format);
  }
}
