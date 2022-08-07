import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { BooksListItemModel } from '../../../core/models/book-list-item.model';
import { BookUpdateModel } from '../../../core/models/book-update.model';
import { BooksApiService } from '../../../core/services/books-api.service';
import { UpdateListService } from '../../../core/services/update-list.service';
import { BookListTabsEnum } from '../shared/enums/book-list-tabs.enum';

@Component({
  selector: 'app-books-page',
  templateUrl: './books-page.component.html',
  styleUrls: ['./books-page.component.css']
})
export class BooksPageComponent implements OnInit {
  allBooks$: Observable<BooksListItemModel[]>;
  recommendedBooks$: Observable<BooksListItemModel[]>;

  listTab = BookListTabsEnum.All;

  readonly bookListTabsEnum = BookListTabsEnum;

  constructor(
    private booksApiService: BooksApiService,
    private updateListService: UpdateListService,
  ) { }

  ngOnInit(): void {
    this.onUpdateList();

    this.updateListService.updateEvent.subscribe(() => this.onUpdateList());
  }

  onUpdateList() {
    // its better to use store than load all books twice each time
    this.allBooks$ = this.booksApiService.getAllBooks();
    this.recommendedBooks$ = this.booksApiService.getRecommended();
  }

  selectTab(option: BookListTabsEnum) {
    this.listTab = option;
  }

  onBookAdd(model: BookUpdateModel) {
    this.booksApiService.saveBook(model)
      .subscribe(() => this.updateListService.emitUpdate())
  }
}
