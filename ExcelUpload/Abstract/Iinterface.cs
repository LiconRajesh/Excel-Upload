using ExcelUpload.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUpload.Abstract
{
  public  interface Iinterface
    {

        int SaveEmployee(List<EmployeeExcel> resultlist);

    }
}
