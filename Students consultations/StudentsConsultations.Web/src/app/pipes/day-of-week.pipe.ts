import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'dayOfWeek' })
export class DayOfWeekPipe implements PipeTransform {

  transform(dayNumber: number): string {
    switch (dayNumber) {
      case 0: {
        return 'Ponedeljak';
      }
      case 1: {
        return 'Utorak';
      }
      case 2: {
        return 'Sreda';
      }
      case 3: {
        return 'Cetvrtak';
      }
      case 4: {
        return 'Petak';
      }
      case 5: {
        return 'Subota';
      }
      case 6: {
        return 'Nedelja';
      }
    }
  }
}
