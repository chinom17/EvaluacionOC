import { AbstractControl, ValidatorFn } from '@angular/forms';

function equalsValidator(validateControl: AbstractControl): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } => {
    const value: any = control.value;
    const otherValue: any = validateControl.value;
    return otherValue === value ? null : { 'notEquals': { value, otherValue } };
  };
}

export const CustomValidators = {
  equals: equalsValidator
};


