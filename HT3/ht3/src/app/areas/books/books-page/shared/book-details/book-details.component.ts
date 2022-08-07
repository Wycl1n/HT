import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';

import { BookInfoModel } from '../../../../../core/models/book-info.model';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BookDetailsComponent implements OnInit {
  private _model: BookInfoModel;
   get model(): BookInfoModel {
    return this._model;
  }
  @Input() set model(value: BookInfoModel) {
    this._model = value;
    this.cd.detectChanges();
  }

  constructor(
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
  }

}
