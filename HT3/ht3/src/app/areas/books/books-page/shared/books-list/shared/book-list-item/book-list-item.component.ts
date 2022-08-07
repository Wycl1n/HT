import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BookInfoModel } from '../../../../../../../core/models/book-info.model';

import { BooksListItemModel } from '../../../../../../../core/models/book-list-item.model';
import { BooksApiService } from '../../../../../../../core/services/books-api.service';
import { UpdateListService } from '../../../../../../../core/services/update-list.service';

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.css']
})
export class BookListItemComponent implements OnInit {
  @Input() model: BooksListItemModel;

  bookDetails$: Observable<BookInfoModel>;

  showDetails = false;
  showEdit = false;

  constructor(
    private bookApiService: BooksApiService,
    private updateListService: UpdateListService,
  ) { }

  ngOnInit(): void {
    this.bookDetails$ = this.bookApiService.getBookModel(this.model.bookId);
  }

  onView() {
    this.showDetails = true;
    this.showEdit = false;
  }
  
  onEdit() {
    this.showDetails = false;
    this.showEdit = true;
  }

  closeDetails() {
    this.showDetails = false;
  }

  closeEdit() {
    this.showEdit = false;
  }

  onEditBook(model: BookInfoModel) {
    this.closeEdit();
    this.bookApiService.saveBook(model)
      .subscribe(() => this.updateListService.emitUpdate())
  }
}
