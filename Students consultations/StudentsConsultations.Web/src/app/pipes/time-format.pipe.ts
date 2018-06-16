import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'timeFormat' })
export class TimeFormatPipe implements PipeTransform {

  transform(date: string | Date, format: string = 'HH:mm'): string {
    return moment.utc(date).local().format(format);
  }
}
