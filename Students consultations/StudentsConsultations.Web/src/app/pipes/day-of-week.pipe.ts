import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'dayOfWeek' })
export class DayOfWeekPipe implements PipeTransform {

  transform(dayNumber: number): string {
    switch (dayNumber) {
      case 1: {
        return 'Ponedeljak';
      }
      case 2: {
        return 'Utorak';
      }
      case 3: {
        return 'Sreda';
      }
      case 4: {
        return 'Cetvrtak';
      }
      case 5: {
        return 'Petak';
      }
      case 6: {
        return 'Subota';
      }
      case 7: {
        return 'Nedelja';
      }
    }
  }
}
