import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ESMatcher } from '../../shared/helpers/esmatcher';
import { CustomValidators } from '../../shared/helpers/validadores';
import { EventArgsInput } from '../../shared/helpers/event-args-input';


@Component({
  selector: 'ev-input-password',
  templateUrl: './input-password.component.html',
  styleUrls: ['./input-password.component.css']
})
export class InputPasswordComponent implements OnInit {
  passConfirm: string;
  pass: string;
  @Output()
  blur = new EventEmitter<EventArgsInput>();

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{9,}')
  ]);
  matcher = new ESMatcher();
  comprobarPasswordFC = new FormControl('', [
    CustomValidators.equals(this.passwordFormControl),
    Validators.required
  ]);

  constructor() { }

  ngOnInit() {
  }
  onBlur($event) {
    let valid = false;
    if (this.passwordFormControl.valid && this.comprobarPasswordFC.valid) {
      valid = true;
    }
    const eventArgs: EventArgsInput = {
      value: $event.target.value,
      valid: valid
    };
    this.blur.emit(eventArgs);
  }
}
