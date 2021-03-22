using APINumbering.Models;
using APINumbering.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace APINumbering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberingController : ControllerBase
    {
        private readonly Conns conns;
       
        private string path;
        private CompaniesData dt = new CompaniesData();
        private StringBuilder sb;
        private IConfiguration Configuration;
        private SqlConnection connString;
        public NumberingController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            conns = new Conns();
            path = Environment.CurrentDirectory;
            path += "\\Numbering_Data_BasedOnSONumber";
            connString =new SqlConnection(this.Configuration.GetConnectionString("MyString"));

            

        }
        [HttpGet]
        public IEnumerable<CompaniesData> Get()
        {
            try
            {

                return conns.GetAll(connString);
            }
            catch(Exception)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public  CompaniesData Get(string id)
        {
            try
            {
             return conns.GetLastGenByInit(connString, id);

               
            }
            catch(Exception) 
            { return null;
            }
        }
        [HttpPost("{val}/{so}/{withpre}")]
        public IActionResult Update(int val,string so,bool withpre, [FromBody] CompaniesData data)
        {
            try
            {
                string id = data.compInitial;
                GetFile gp = new GetFile();
                sb = gp.Getfile(withpre, data.WithScratched, data.Sufix_Scratch, data.Scr_len, data.compInitial, data.lastGenNumber, val, data.totalCols, data.codeDiffs, data.upsPerSheet, data.isVertical, data.spaceBetweenSheets, data.colCntr);

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                System.IO.File.WriteAllText(path + "\\" + so + "_" + Convert.ToInt32(data.lastGenNumber + 1) + "_" + val + ".csv", sb.ToString());
                conns.SOInsertUpdate(connString,data.companyName, so, Convert.ToInt32(data.lastGenNumber + 1), val, id);
                return Ok(new { ResponseCode = 200, ResponseMessage = "Record Updated" });
                //  Response.Redirect(path + "\\" + data.SONumber + "_" + data.lastGenNumber + "_" + val + ".csv");

            }
            catch(Exception ex)
            {
                return Ok(new { ResponseCode = 200, ResponseMessage = "Record Found", ResponseData = ex });

            }

        }
        [HttpGet("download/{so}/{from}/{to}")]
        public   FileContentResult Download(string so, int from, int to)
        {
            try
            {
                byte[] bytes;
                using (FileStream fs = new FileStream(path + "\\" + so + "_" + from + "_" + to + ".csv", FileMode.Open, FileAccess.Read))
                {
                    bytes = System.IO.File.ReadAllBytes(path + "\\" + so + "_" + from + "_" + to + ".csv");
                }
                return new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "QR's_SO" + so + "_" + from + "_" + to + ".csv"
                };

            }
            catch (Exception ex)
            {
                return null;


            }

        }
        [HttpGet("filebySO/{so}")]
        public FileContentResult GetFileBySo(string so)
        {

            try
            {
                Sonumber num = conns.GetSODetails(connString, so);
                byte[] bytes;
                using (FileStream fs = new FileStream(path + "\\" + so + "_" + num.fromNo + "_" + num.ToNo + ".csv", FileMode.Open, FileAccess.Read))
                {
                    bytes = System.IO.File.ReadAllBytes(path + "\\" + so + "_" + num.fromNo + "_" + num.ToNo + ".csv");
                }
                return new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "QR's_SO" + so + "_" + num.fromNo + "_" + num.ToNo + ".csv"
                };
            }
            catch(Exception)
            {
                return null;
            }
        }


    }
}
