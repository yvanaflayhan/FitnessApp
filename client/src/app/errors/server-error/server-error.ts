import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-server-error',
  imports: [CommonModule],
  templateUrl: './server-error.html',
  styleUrl: './server-error.css',
})
export class ServerError {
  error: any;
  constructor() {
    this.error = history.state.error;
    history.replaceState({}, '');
  }

}
