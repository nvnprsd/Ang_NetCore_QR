using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APINumbering.Models
{
    public class Conns
    {
       
       
         public IEnumerable<CompaniesData> GetAll(SqlConnection connect)//get the all company name and initials from table
        {
            try
            {
                using (IDbConnection dbConnection = connect)
                {
                    dbConnection.Open();
                    return dbConnection.Query<CompaniesData>("select CompanyName,compInitial from CompaniesData order by companyName");
                }
            }
            catch(Exception)
            {
                return null;

            }
        }
        public CompaniesData GetLastGenByInit(SqlConnection connect,string id)//return the last generated value of company with initials of string ID
        {
               
            try { 
            using (IDbConnection dbConnection = connect)
            {
                return dbConnection.Query<CompaniesData>("select * from CompaniesData where compInitial=@Id", new { Id = id }).FirstOrDefault();
            }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string dt = DateTime.Now.ToLongDateString();
     
        public void SOInsertUpdate(SqlConnection connect,string CmpName,string so, int from,int to, string id)//update the new saved  last genrated value in database
        {
           // try{
                using (IDbConnection dbConnection = connect)
            {
                dbConnection.Open();
                dbConnection.Query<Sonumber>("insert into SORecords(SONumber,FromNo,ToNo,CompanyName) values(@so,@from,@to,@cmp)", new { so = so, from = from,to=to,cmp=CmpName });
                dbConnection.Query<CompaniesData>("update CompaniesData set lastGenNumber=@val,LastModifyDate=@dt where compInitial=@id", new { Id = id, val = to, dt = dt });


            }

        }

        public Sonumber  GetSODetails(SqlConnection connect, string so)//update the new saved  last genrated value in database
        {
           // try            {
                using (IDbConnection dbConnection = connect)
                {
                    dbConnection.Open();
                    string dt = DateTime.Now.ToLongDateString();
                    return dbConnection.Query<Sonumber>("select FromNo,ToNo from SORecords where SONumber=@so", new { so = so }).FirstOrDefault();

                }
            
           
        }
        public string Register(SqlConnection connect, CompaniesData data)//register company into dbase
        {

            using (IDbConnection dbConnection = connect)
            {
                dbConnection.Open();
                string dt = DateTime.Now.ToLongDateString();
                dbConnection.Query<CompaniesData>("insert into CompaniesData(companyName,CompInitial,lastGenNumber,totalCols,codeDiffs,upsPerSheet,isVertical,spaceBetweenSheets,colCntr,WithScratched,Sufix_Scratch,Scr_len) values(@cn,@Ci,@lGN,@tC,@cD,@uPS,@iV,@sBS,@cC,@WS,@SS,@Sl)", new { @cn = data.companyName, @Ci = data.compInitial, @lGN = data.lastGenNumber, @tC = data.totalCols, @cD = data.codeDiffs, @uPS = data.upsPerSheet, @iV = data.isVertical, @sBS = data.spaceBetweenSheets, @cC = data.colCntr, @WS = data.WithScratched, @SS = data.Sufix_Scratch, @Sl = data.Scr_len });

            }
            return "OK";
        }
        public string Update(SqlConnection connect, CompaniesData data)//register company into dbase
        {

            using (IDbConnection dbConnection = connect)
            {
                dbConnection.Open();
                string dt = DateTime.Now.ToLongDateString();
                dbConnection.Query<CompaniesData>("update CompaniesData set companyName=@cn,lastGenNumber=@lGN,totalCols=@tC,codeDiffs=@cD,upsPerSheet=@uPS,isVertical=@iV,spaceBetweenSheets=@sBS,colCntr=@cC,WithScratched=@WS,Sufix_Scratch=@SS,Scr_len=@Sl where CompInitial=@Ci", new { @cn = data.companyName, @Ci = data.compInitial, @lGN = data.lastGenNumber, @tC = data.totalCols, @cD = data.codeDiffs, @uPS = data.upsPerSheet, @iV = data.isVertical, @sBS = data.spaceBetweenSheets, @cC = data.colCntr, @WS = data.WithScratched, @SS = data.Sufix_Scratch, @Sl = data.Scr_len });

            }
            return "OK";
        }
        public IEnumerable<Cdetails> getallC(SqlConnection con)
        {
            using (IDbConnection dbConnection = con)
            {
                dbConnection.Open();
                return dbConnection.Query<Cdetails>("select CompanyName as Company,lastGenNumber as LastGen,LastModifyDate as LastModify from CompaniesData order by LastModifyDate desc");

            }
        }
        public IEnumerable<SOdetails> getallSO(SqlConnection con)
        {
            using (IDbConnection dbConnection = con)
            {
                dbConnection.Open();
                return dbConnection.Query<SOdetails>("select CompanyName as Company,fromNo as [From],ToNo as Upto,GenDate from SORecords order by GenDate desc");

            }
        }

    }
}
