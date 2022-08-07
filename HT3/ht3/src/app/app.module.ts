import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BooksPageComponent } from './areas/books/books-page/books-page.component';
import { BooksListComponent } from './areas/books/books-page/shared/books-list/books-list.component';
import { BookListItemComponent } from './areas/books/books-page/shared/books-list/shared/book-list-item/book-list-item.component';
import { BookDetailsComponent } from './areas/books/books-page/shared/book-details/book-details.component';
import { ModalComponent } from './shared/modal/modal.component';
import { ReviewsListComponent } from './areas/books/books-page/shared/book-details/shared/reviews-list/reviews-list.component';
import { ReviewItemComponent } from './areas/books/books-page/shared/book-details/shared/reviews-list/shared/review-item/review-item.component';
import { BookFormComponent } from './areas/books/books-page/shared/book-form/book-form.component';
import { GetCountPipe } from './core/pipes/get-count.pipe';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    BooksPageComponent,
    BooksListComponent,
    BookListItemComponent,
    BookDetailsComponent,
    ModalComponent,
    ReviewsListComponent,
    ReviewItemComponent,
    BookFormComponent,
    GetCountPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en',
      useDefaultLang: true,
      loader: {
          provide: TranslateLoader,
          useFactory: createTranslateLoader,
          deps: [HttpClient],
      },
    }),
    AppRoutingModule
  ],
  entryComponents: [BookDetailsComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
