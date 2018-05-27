import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'dateFormat' })
export class DateFormatPipe implements PipeTransform {

  transform(date: string | Date, format: string = 'HH:mm'): string {
    return moment.utc(date).format(format);
  }
}
