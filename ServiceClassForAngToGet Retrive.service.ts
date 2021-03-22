 import { map } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import { GetNumbers } from './get-numbers.model';
import { HttpClient} from '@angular/common/http'
import { ToastrService } from 'ngx-toastr';
import {environment} from 'src/environments/environment'

    @Injectable({
    providedIn: 'root'
    })
   export class GetNumbersService {

    constructor(private http:HttpClient,private toastr:ToastrService) {}
    from:GetNumbers=new GetNumbers();
    compList:GetNumbers[];
    options=[];
    compde:GetNumbers[];

    filepath= environment.baseUrl+"Numbering/download"
    filebySO=environment.baseUrl+"Numbering/filebySO"

    getAllCompanyNames(){
      return this.http.get(environment.baseUrl+"Numbering").toPromise().then(res=>{this.compList=res as GetNumbers[]


if(this.compList!=undefined )
{
  this.toastr.success("Response Registered","DB Server Online")}
else{this.toastr.error("Unable to find Server","DB Server Offline")}
      },error=>{
        this.toastr.error("No Response from Server","DB Server Error")

      }
      )

    }
    getCompDetails(id:string){
     return this.http.get(`${environment.baseUrl}Numbering/${id}`).toPromise().then(res=>{this.compde=res as GetNumbers[]


    })


    }
    getFileUrl(val:number,so:string,fromNo:number,withprefix:boolean)
    {
           this.from=Object.assign(this.compde)
           this.http.post(`${environment.baseUrl}Numbering/${val}/${so}/${withprefix}`,this.from).toPromise().then(res=>
       {
          window.open(`${this.filepath}/${so}/${fromNo}/${val}`)
          this.toastr.success("Sending file from Server..!","File Download Started")
          setTimeout(() => {
            location.reload();

          }, 1500);
         }
         ,err=>
         {
           this.toastr.error("Server Communication Failure..! Find Complete Error In Console","DB Server out of Reach")
         });
    }

        getbySO(so:string)
        {
          window.open(`${this.filebySO}/${so}`)
          this.toastr.success("Your File Sent..!","File Downloaded ")
        }
        registerCompany(data)
        {
          console.log("sending data");

          this.http.post(`${environment.baseUrl}registerComp`,data).toPromise().then(res=>
          {
            this.toastr.success("Company Details Saved","Registered..!")
            setTimeout(() =>
            {
              location.reload();

            }, 1500);
          },err=>
          {
           this.toastr.error("Communication Failure","DB Server out of reach")
          })
        }
    UpdateComp(data)
    {
       this.http.put(`${environment.baseUrl}registerComp`,data).toPromise().then(res=>
      {
        this.toastr.success("Details Saved","Update Successfull..!")
        setTimeout(() =>
        {
          location.reload();

        }, 1500);
      },err=>
      {
       this.toastr.error("Communication Failure","DB Server out of reach")
      })
    }
    ViewCompnies(type:string)
    {
      return this.http.get(`${environment.baseUrl}registerComp/${type}`).toPromise().then(res=>this.compde=res as GetNumbers[])
    }

FileUpload(fileToUpload:File){
  const formData: FormData = new FormData();
  this.toastr.info("file Uploading & Conversion In progress Please wait according File Size @15 values/Sec","Uploading & Converting",{})
  formData.append('fileKey', fileToUpload, fileToUpload.name);
    return this.http
      .post(`${environment.baseUrl}registerComp/Files`, formData)
      .toPromise().then(() => {
        this.toastr.show("Converted Succesfully  Sending file on your Port","File Converted")
         window.open(`${environment.baseUrl}registerComp/PDF`) })
      .catch((e) => console.log(e));
}
}
