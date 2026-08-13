import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { AdminService } from '../../_services/admin';

@Component({
  selector: 'app-admin-users',
  imports: [],
  templateUrl: './admin-users.html',
  styleUrl: './admin-users.css',
})
export class AdminUsers implements OnInit{
  users: Member[] =[];

  constructor (private adminService: AdminService){}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.adminService.getUsers().subscribe({
      next: users => {
        this.users = users;
      },
      error: error=> {
        console.log(error) 
      }
    }

    )

  }


}
