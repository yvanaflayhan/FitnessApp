import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';
import { Nav } from "./nav/nav";
import { FormsModule } from '@angular/forms';
import { User } from './_models/user';
import { Account } from './_services/account';
import { Home } from "./home/home";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Nav, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected readonly title = signal('client');

  users: any; 

  constructor( private account: Account) {}
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString= localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString);
    this.account.setCurentUser(user);
  }
}
