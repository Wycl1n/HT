import { EventEmitter, Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class UpdateListService {

    public updateEvent = new EventEmitter();

    public emitUpdate() {
        this.updateEvent.emit();
    }
}  