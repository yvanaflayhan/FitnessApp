import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { AdminService } from '../../_services/admin';
import { FormsModule } from '@angular/forms';
import { Pagination } from '../../shared/pagination/pagination';

@Component({
  selector: 'app-admin-users',
  imports: [FormsModule, Pagination],
  templateUrl: './admin-users.html',
  styleUrl: './admin-users.css',
})
export class AdminUsers implements OnInit {
  users: Member[] = [];

  currentPage = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 1;

  showEditForm = false;
  editingUser: Member | null = null;

  constructor(private adminService: AdminService, private changeDetector: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.adminService.getUsers(this.currentPage, this.pageSize).subscribe({
      next: result => {
        this.users = result.items;
        this.totalCount = result.totalCount;
        this.totalPages = Math.ceil(this.totalCount / this.pageSize);

        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error loading users:', error);
      }
    });
  }

  onPageChanged(page: number): void {
    this.currentPage = page;
    this.loadUsers();
  }

  editUser(user: Member): void {
    this.editingUser = { ...user };
    this.showEditForm = true;
    this.changeDetector.detectChanges();
  }

  cancelEdit(): void {
    this.showEditForm = false;
    this.editingUser = null;
  }

  saveEdit(): void {
    if (!this.editingUser) {
      return;
    }
    this.adminService.updateUser(
      this.editingUser.id,
      this.editingUser).subscribe({
        next: updateUser => {
          const index = this.users.findIndex(
            user => user.id === updateUser.id
          );
          if (index !== -1) {
            this.users[index] = updateUser;
          }
          this.showEditForm = false;
          this.editingUser = null;
          this.changeDetector.detectChanges();
        },
        error: error => {
          console.error('Error updating user:', error);
        }
      });
  }

  deleteUser(id: number): void {
    if (!confirm('Are you sure you want to delete this user?')) {
      return;
    }
    this.adminService.deleteUser(id).subscribe({
      next: () => {
        this.users = this.users.filter(
          user => user.id !== id
        );
        this.changeDetector.detectChanges();
      },
      error: error => {
        console.error('Error deleting user:', error);
      }
    });
  }
}
