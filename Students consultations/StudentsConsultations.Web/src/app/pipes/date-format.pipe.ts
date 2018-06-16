import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'dateFormat' })
export class DateFormatPipe implements PipeTransform {

  transform(date: string | Date, format: string = 'DD-MM-YYYY'): string {
    return moment.utc(date).local().format(format);
  }
}
