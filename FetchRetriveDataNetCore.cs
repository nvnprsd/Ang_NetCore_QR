using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APINumbering.Models;
using APINumbering.Shared;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Drawing;
using QRCoder;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace APINumbering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerCompController : Controller
    {
        private IConfiguration Configuration;
        private readonly Conns conn;
        private readonly getPDF getpdf;
        private SqlConnection connString;
        public registerCompController(IConfiguration _configuration)
        {
            conn = new Conns();
            getpdf = new getPDF();

            Configuration = _configuration;
            connString = new SqlConnection(this.Configuration.GetConnectionString("MyString"));
        }

        [HttpPost]
        public void RegisterComp([FromBody] CompaniesData data)
        {
            conn.Register(connString, data);

        }
        [HttpPut]
        public void UpdateComp([FromBody] CompaniesData data)
        {
            conn.Update(connString, data);

        }
        [HttpGet("Cdetails")]
        public IEnumerable<Cdetails> GetCdetails()
        {
            return conn.getallC(connString);
        }
        [HttpGet("SOdetails")]
        public IEnumerable<SOdetails> GetSOdetails()
        {
            return conn.getallSO(connString);
        }


        [HttpPost("Files")]
        public IActionResult Uploader()
        {
            //try
            //   Directory.Delete(Environment.CurrentDirectory+ "\\CSV_to_QR\\");
            var file = Request.Form.Files[0];
            string path = Directory.GetCurrentDirectory() + "\\CSV_to_QR";

            if (!System.IO.Directory.Exists(path))
            {

                System.IO.Directory.CreateDirectory(path);
            }
            var fullPath = "";
            if (file.Length > 0)
            {
                var fileName = "abc.csv";

                fullPath = System.IO.Path.Combine(path, fileName.ToString());
                var dbPath = fileName.ToString();
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
              getpdf.GetQr(path);
          // getpdf.GenPdf(path);
            
            return Ok();
        }
        [HttpGet("PDF")]
        public FileContentResult DownloadPDF()
        {
                string path = Directory.GetCurrentDirectory() + "\\CSV_to_QR\\SV_to_QR.pdf";
                byte[] bytes;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    bytes = System.IO.File.ReadAllBytes(path);
                }
               
                return new FileContentResult(bytes, "application/pdf")
                {
                    FileDownloadName = "CSVtoQR.pdf"
                };

           

        }
    }
}

