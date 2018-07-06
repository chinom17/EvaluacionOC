import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';



@Component({
  selector: 'ev-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export class SelectComponent implements OnInit {
  @Input()
  Source: any[];
  @Input()
  placeholder: string;
  // tslint:disable-next-line:no-output-on-prefix
  @Output()
  onSelect = new EventEmitter<any>();
  @Input()
  value: any;
  constructor() { }
  ngOnInit() {
  }
  onClick() {
    this.onSelect.emit(this.value);
  }

}
