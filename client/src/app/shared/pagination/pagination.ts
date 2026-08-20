import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OutletContext } from '@angular/router';

@Component({
  selector: 'app-pagination',
  imports: [],
  templateUrl: './pagination.html',
  styleUrl: './pagination.css',
})
export class Pagination {

  @Input() currentPage = 1;
  @Input() totalPages = 1;

  @Output() pageChanged = new EventEmitter<number>();

  goToPage(page: number): void{
    if(page < 1 || page > this.totalPages){
      return;
    }

    this.pageChanged.emit(page);
  }


}
