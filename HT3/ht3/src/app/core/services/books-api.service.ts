import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { OrderBooksByEnum } from '../enums/order-books-by.enum';
import { BookInfoModel } from '../models/book-info.model';
import { BooksListItemModel } from '../models/book-list-item.model';
import { BookUpdateModel } from '../models/book-update.model';

import { api, ApiService, extractData, formatString } from './api-service.service';

@Injectable({
  providedIn: 'root'
})
export class BooksApiService {

  constructor(
    private http: ApiService
  ) { }

  getAllBooks(order: OrderBooksByEnum = null): Observable<BooksListItemModel[]> {
    const url = api.books.getAll;
    let params = null;
    if (order)
      params = new HttpParams().append('order', order);
    
    return this.http.get(url, { params }).pipe(extractData(true));
  }

  getBookModel(bookId: number): Observable<BookInfoModel> {
    const url = formatString(api.books.getModelById, { id: bookId });

    return this.http.get(url).pipe(extractData(true));
  }

  saveBook(model: BookUpdateModel): Observable<number> {
    const url = api.books.createOtUpdate;

    return this.http.post(url, model).pipe(extractData(true));
  }

  getRecommended(genre: string = null): Observable<BooksListItemModel[]> {
    const url = api.recommended.getTopTen;
    let params = null;
    if (genre)
      params = new HttpParams().append('genre', genre);
    
    return this.http.get(url, { params }).pipe(extractData(true));
  }
}
