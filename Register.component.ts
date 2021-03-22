import { ToastrService } from 'ngx-toastr';
import { NgForm,ReactiveFormsModule ,FormControl} from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import{GetNumbersService} from '../Shared/get-numbers.service'
import {GetNumbers} from '../Shared/get-numbers.model'
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  type:any=new GetNumbers();
  selected:string;
  chk:boolean=true
  updateT:boolean=false;
  BtnValue="Submit"




constructor( public Service:GetNumbersService,private toster:ToastrService){}
keyCode(event)
{if(event.keyCode){}
}
Fetch(){
this.updateT=true;
this.BtnValue="Update Info"
 this.Service.getCompDetails(this.selected).then(res=>{this.type=Object.assign({},this.Service.compde)
})

}
onsubmit(data:NgForm){
if(data.valid)
{
    if(!this.type.withScratched)
    {
      this.type.scr_len=0;
      this.register();

    }
    else
    {
      let num=this.type.scr_len;
      if(num>=4)
      {
      this.register();
      }
      else{
        this.toster.error("Scratch Length is Too Short","Error")
      }

    }
 }
 else{
   this.toster.error("All Required Details Must be filled","Failed..!")
 }
}
register()
{
  if(this.BtnValue=="Submit")
        {
        this.Service.registerCompany(this.type);

        }
        else{
          this.Service.UpdateComp(this.type)
        }


}

ngOnInit() {
}
}
