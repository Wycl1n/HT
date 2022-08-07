import { Component, Input, OnInit } from '@angular/core';

import { BooksListItemModel } from '../../../../../core/models/book-list-item.model';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit {
  @Input() booksList: BooksListItemModel[];

  constructor() { }

  ngOnInit(): void {
  }

}
