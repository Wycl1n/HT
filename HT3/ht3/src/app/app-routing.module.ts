import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BooksPageComponent } from './areas/books/books-page/books-page.component';

const routes: Routes = [
  { path: '', component: BooksPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
