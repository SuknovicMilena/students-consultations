import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'dateFormat' })
export class DateFormatPipe implements PipeTransform {

  transform(date: string | Date, format: string = 'YYYY-MM-DD'): string {
    return moment.utc(date).subtract(1, 'month').local().format(format);
  }
}
