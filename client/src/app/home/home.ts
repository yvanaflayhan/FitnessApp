import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { Register } from "../register/register";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  imports: [NgIf, Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => { console.log(response); this.users = response; },//I added the {} and the console.log 
      error: error => console.log(error),
      complete: () => console.log('Request has completed ')

    })
  }

  cancelRegisterMode( event: boolean){
    this.registerMode = event;
  }
}
