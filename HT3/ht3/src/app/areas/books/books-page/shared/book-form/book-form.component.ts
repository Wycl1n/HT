import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { BookInfoModel } from '../../../../../core/models/book-info.model';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css'],
})
export class BookFormComponent implements OnInit {
  
  private _model: BookInfoModel;
  get model(): BookInfoModel {
    return this._model;
  }
  @Input() set model(value: BookInfoModel) {
    this._model = value;
    this.setupForm();
  }

  bookForm: FormGroup;
  isValid = true;

  @Output() commitEvent = new EventEmitter<BookInfoModel>();

  constructor(
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.setupForm();
  }

  private setupForm() {
    //its better to build forms on backend
    this.bookForm = this.formBuilder.group({
      bookId: new FormControl(this.model?.bookId),
      title: new FormControl(this.model?.title, [Validators.required, Validators.maxLength(100)]),
      cover: new FormControl(this.model?.cover, [Validators.required]),
      genre: new FormControl(this.model?.genre, [Validators.required, Validators.maxLength(100)]),
      author: new FormControl(this.model?.author, [Validators.required, Validators.maxLength(100)]),
      content: new FormControl(this.model?.content, [Validators.required, Validators.maxLength(1000)])
    });
  }

  onCommit() {
    this.bookForm.markAsTouched();
    
    if (this.bookForm.invalid) {
      this.isValid = false;
      return;
    }

    this.isValid = true;
    this.bookForm.controls['bookId'].setValue(this.model?.bookId);
    this.commitEvent.emit(this.bookForm.value);
    this.bookForm.reset();
  }

  onClear() {
    this.isValid = true;
    this.bookForm.reset();
    (document.getElementById('fileInput') as any).value = null;
  }

  handleUpload(event) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.bookForm.controls['cover']
        .setValue(reader.result);
    };
  }
}
