import {
    Component,
    Output,
    EventEmitter,
    OnChanges,
    Input
} from '@angular/core';

@Component({
  selector: 'pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnChanges {
    @Input("total-items") totalItems: number;
    @Input("page-size") pageSize: number = 10;
    @Output("page-changed") pageChanged = new EventEmitter();
    pages: any[];
    currentPage = 1;

  constructor() { }

  ngOnChanges() {
      this.currentPage = 1;

      var pageCount = Math.ceil(this.totalItems / this.pageSize);
      this.pages = [];
      for (var i = 1; i <= pageCount; i++) {
          this.pages.push(i);
      }
  }
  changePage(page: number) {
      this.currentPage = page;
      this.pageChanged.emit(page);
  }
  previous() {
      if (this.currentPage == 1)
          return;
      this.currentPage--;
      this.pageChanged.emit(this.currentPage);
  }
  next() {
      if (this.currentPage == this.pages.length)
          return;
      this.currentPage++;
      this.pageChanged.emit(this.currentPage);
  }

}
