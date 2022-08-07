import { Component, Input, OnInit } from '@angular/core';

import { ReviewInfoModel } from '../../../../../../../core/models/review-info.model';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {
  @Input() reviews: ReviewInfoModel[];

  constructor() { }

  ngOnInit(): void {
  }
}
