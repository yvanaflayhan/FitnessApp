import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../_services/account';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  constructor(private account: Account, private toastr: ToastrService) {}

  ngOnInit(): void {
    
  }
  register(){
    this.account.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => {
        this.toastr.error(error.error);
        console.log(error);
      }
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
