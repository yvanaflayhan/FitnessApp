import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';
import { Account } from '../_services/account';

@Component({
  selector: 'app-register',
  imports: [FormsModule, NgFor],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  constructor(private account: Account) {}

  ngOnInit(): void {
    
  }
  register(){
    this.account.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => console.log(error)
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
