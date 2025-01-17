﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class EmployeeController : ApiController

        //GET
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @" select EmployeeID, EmployeeName, Department, MailID,
                              convert(varchar(10), DOJ, 120) as DOJ 
                              from dbo.Employees
                           ";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        //POST
        public string Post(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" 
                            insert into dbo.Employees
                            (EmployeeName,
                             Department,
                             MailID,
                             DOJ)
                           Values
                           (
                            '" +emp.EmployeeName + @"'
                           ,'" + emp.Department + @"'
                           ,'"+ emp.MailID + @"'
                           ,'"+ emp.DOJ + @"'
                            )
                           ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Added successfully";
            }
            catch
            {
                return "Failed to Add";
            }
        }


        //PUT
        public string Put(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" 
                            update dbo.Employees set 
                            EmployeeName =  '" + emp.EmployeeName + @"'
                           ,Department =  '" + emp.Department + @"'
                           ,MailID =  '" + emp.MailID + @"'
                           ,DOJ =  '" + emp.DOJ + @"'
                            where EmployeeID = " + emp.EmployeeID + @"
                               ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Updated successfully";
            }
            catch
            {
                return "Failed to Update";
            }
        }

        //delete
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" 
                            delete from dbo.Employees where EmployeeID = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Deleted successfully";
            }
            catch
            {
                return "Failed to Delete";
            }
        }

    }
}
