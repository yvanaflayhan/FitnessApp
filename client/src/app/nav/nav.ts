import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Account } from '../_services/account';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-nav',
  imports: [FormsModule, CommonModule, BsDropdownModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav implements OnInit{
  model: any ={}
  loggedIn = false;
  currentUser$: Observable<User | null> = of(null);

  constructor(public account: Account, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.currentUser$ = this.account.currentUser$;
  }


  login(){
    console.log(this.model);
    this.account.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/gyms'),
    })
  }

  logout(){
    this.account.logout();
    this.router.navigateByUrl('/');
  }

}
