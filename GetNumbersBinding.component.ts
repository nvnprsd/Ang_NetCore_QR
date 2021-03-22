import { Component, OnInit } from '@angular/core';
import { NgForm, ReactiveFormsModule} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GetNumbersService } from '../Shared/get-numbers.service';

@Component({
  selector: 'app-get-numbers',
  templateUrl: './get-numbers.component.html',
  styles: [
  ]
})
export class GetNumbersComponent implements OnInit {
  numToGen:number
    SelVal:any
  fromNo:number
  lastGen:number
  SONumber:string
  disable:boolean=false;
  withprefix:boolean=false;
  constructor(public service:GetNumbersService,private tostr:ToastrService) { }

  ngOnInit(): void {
this.service.getAllCompanyNames();

  }


onSave(){
  this.disable=false;
  this.service.getCompDetails(this.SelVal).then(res=>{
    this.fromNo= this.service.compde['lastGenNumber']
     this.fromNo=this.fromNo+1

   });

}


onsubmit(data:NgForm){
  if(data.valid)
  {


    this.tostr.info(`We are Creating file @ 15K Number/Seconds Getting File Ready`  ,"Please Wait ")
    this.disable=true;
  this.service.getFileUrl(this.numToGen+this.fromNo-1,this.SONumber,this.fromNo,this.withprefix)
  }
  else{
   this.tostr.error("Please Enter Required Credentials","Oh...!")
  }

}


CallForSo(data)
{
if(typeof data =='undefined')
{
  this.tostr.error("Please Enter Required Credentials","Oh...!")

}

    this.service.getbySO(this.SONumber)

}


}
