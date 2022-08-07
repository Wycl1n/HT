import { Component, Input, OnInit } from '@angular/core';

import { ReviewInfoModel } from '../../../../../../../../../core/models/review-info.model';

@Component({
  selector: 'app-review-item',
  templateUrl: './review-item.component.html',
  styleUrls: ['./review-item.component.css']
})
export class ReviewItemComponent implements OnInit {
  @Input() model: ReviewInfoModel

  constructor() { }

  ngOnInit(): void {
  }
}
