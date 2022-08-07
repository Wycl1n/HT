import { Component, ContentChild, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit  {
  @ContentChild('header') header: TemplateRef<any>;
  @ContentChild('body') body: TemplateRef<any>;

  @Output() closeModalEvent = new EventEmitter();

  constructor() { }
  
  ngOnInit(): void {
  }

  closeModal() {
    this.closeModalEvent.emit();
  }
}
