import { Component, OnInit } from '@angular/core';
import {GetNumbersService} from '../Shared/get-numbers.service'
import {ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-csv-toqr',
  templateUrl: './csv-toqr.component.html',
  styleUrls: ['./csv-toqr.component.css']
})
export class CsvTOqrComponent implements OnInit {
view :boolean=false;
time:any="";
  fileToUpload: File = null;
  constructor(private service: GetNumbersService,private tostr:ToastrService) { }


  ngOnInit() {
  }



  handleFileInput(file: File) {
    if (file.type == "application/vnd.ms-excel") {

   this.fileToUpload=file;
let a=this.fileToUpload.size/60000

this.time=a
if(a>300)
{


  this.tostr.info(`EPT is Longer You have to wait Until we finish..! `,"Ready to Upload File")
    }
    else{

      this.tostr.info(`EPT is Short, Good To Go..!`,"Ready to Upload File")


    }
  }
    else {
      this.tostr.error("Invalid File Type","Wrong File")
    }


  }

  Upload()
  {
    if(this.fileToUpload!=null)
    {
      this.view=true;
    this.service.FileUpload(this.fileToUpload).then(res=>{this.view=false
    });
    }
    else{

      this.tostr.error("Please Select a file","File not found")

    }
  }
}
