import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { AdminService } from '../_services/admin';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin',
  imports: [CommonModule],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin implements OnInit {
  users: Member[]=[];

  constructor(private adminService: AdminService){}

  ngOnInit(): void {
    this.loadUsers();
  }
  loadUsers(): void{
    this.adminService.getUsers().subscribe({
      next: users =>{
        this.users = users;
      },
      error: error =>{
        console.log(error);
      }
    });
  }

}
