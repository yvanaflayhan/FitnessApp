import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected readonly title = signal('client');

  users: any; 

  constructor(private http : HttpClient) {}
  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => { console.log(response); this.users = response;},//I added the {} and the console.log 
      error: error => console.log(error),
      complete: () => console.log('Request has completed ')

    })
  }
}
