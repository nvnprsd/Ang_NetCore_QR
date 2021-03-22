import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { Observable ,} from 'rxjs';
import { startWith, map,toArray } from 'rxjs/operators'
import { GetNumbersService } from '../Shared/get-numbers.service';
import { ChangeDetectorRef } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
@Component({
  selector: 'app-view-all',
  templateUrl: './view-all.component.html',
  styleUrls: ['./view-all.component.css']
})
export class ViewAllComponent implements OnInit {

  @ViewChild('type1') paginator: MatPaginator;
  @ViewChild('type2') paginator2: MatPaginator;
  Cdstate: string = 'Hide';
  Sostate: string = 'Hide';


  dataSource ;
  dataSource2;
  displayedColumns: string[];
  displayedColumns2: string[];
  constructor(private service: GetNumbersService) { }

  ngOnInit(): void {
  }

  getC(data: string) {
    if (data == "0") {

      this.Sostate = "Hide"
      this.Cdstate = "Show"

      this.displayedColumns = ['company', 'lastGen', 'lastModify'];

      this.service.ViewCompnies('Cdetails').then(res => {

console.log(res);

        this.dataSource= new MatTableDataSource([]);
        this.dataSource.data = res;
        this.dataSource.paginator = this.paginator;
      })

    }
    else {
     this.Cdstate = "Hide"
     this.Sostate = "Show"
      this.displayedColumns2 = ['company', 'from', 'upto', 'genDate'];

      this.service.ViewCompnies('SOdetails').then(res => {
        console.log(res);
        this.dataSource2= new MatTableDataSource([]);
        this.dataSource2.data = res;

    this.dataSource2.paginator = this.paginator2;
     })

    }

  }

}
