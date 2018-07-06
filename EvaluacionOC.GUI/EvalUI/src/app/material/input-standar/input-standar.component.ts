import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Validators, FormControl, ValidatorFn } from '@angular/forms';
import { ESMatcher } from '../../shared/helpers/esmatcher';
import { EventArgsInput } from '../../shared/helpers/event-args-input';

@Component({
  selector: 'ev-input-standar',
  templateUrl: './input-standar.component.html',
  styleUrls: ['./input-standar.component.css']
})
export class InputStandarComponent implements OnInit {
  @Input()
  requerido?: boolean;
  @Input()
  minLength?: number;
  @Input()
  nombre?: string;
  @Input()
  placeholder?: string;
  @Input()
  type?: string;
  @Input()
  texto?: string;
  @Output()
  blur = new EventEmitter<EventArgsInput>();
  text: string;


  standarFormControl;
  matcher = new ESMatcher();
  private validadores(): ValidatorFn[] {
    const vals: ValidatorFn[] = [];
    if (this.requerido) {
      vals.push(Validators.required);
    }
    if (this.minLength > 0) {
      vals.push(Validators.minLength(this.minLength));
    }
    return vals;
  }
  constructor() {
    // this.habilitado = true;
  }
  ngOnInit() {
    this.standarFormControl = new FormControl('',
      this.validadores()
    );
  }
  onBlur($event) {
    const eventArgs: EventArgsInput = {
      value: $event.target.value,
      valid: this.standarFormControl.valid
    };
    this.blur.emit(eventArgs);

  }

}
