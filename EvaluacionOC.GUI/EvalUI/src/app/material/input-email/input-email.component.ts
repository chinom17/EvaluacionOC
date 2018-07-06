import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ESMatcher } from '../../shared/helpers/esmatcher';
import { EventArgsInput } from '../../shared/helpers/event-args-input';




@Component({
  selector: 'ev-input-email',
  templateUrl: './input-email.component.html',
  styleUrls: ['./input-email.component.css']
})
export class InputEmailComponent implements OnInit {

  @Input()
  texto: string;
  @Input()
  habilitado: boolean;
  @Output()
  blur = new EventEmitter<EventArgsInput>();

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  matcher = new ESMatcher();
  constructor() { }

  ngOnInit() {
  }
  onBlur($event) {
    const eventArgs: EventArgsInput = {
      value: $event.target.value,
      valid: this.emailFormControl.valid
    };
    this.blur.emit(eventArgs);
  }

}
