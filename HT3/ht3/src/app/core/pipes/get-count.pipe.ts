import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'getCount'
})
export class GetCountPipe implements PipeTransform {

  transform(value: any[]): number {
    console.log(value)
    return value?.length ?? 0;
  }
}
