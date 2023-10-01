using ExcelUpload.Abstract;
using ExcelUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelUpload.concrete
{
    public class Repository : Iinterface
    {
        ExcelDatabaseEntities con = new ExcelDatabaseEntities();
    

        public int SaveEmployee(List<EmployeeExcel> resultlist)
        {
            if (resultlist.ToList().Count > 0)
            {
                foreach (var item in resultlist)
                {



                    con.EmployeeExcels.Add(item);
                    con.SaveChanges();

                }
            }
            return 1;
        }
    }
    }
