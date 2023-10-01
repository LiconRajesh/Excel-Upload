using ExcelUpload.Abstract;
using ExcelUpload.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelUpload.Controllers
{
    public class ExcelUploadController : Controller
    {

        private readonly Iinterface applicationRepository;

        public ExcelUploadController(Iinterface app)
        {
            this.applicationRepository = app;
        }
        // GET: ExcelUpload
        public ActionResult Index()
        {
            return View();
        }

     






        public ActionResult Demo()
        {
            return View();
        }
        //  <add key="EPPlus:ExcelPackage.LicenseContext" value="NonCommercial" />
        //add this in web config 
        [HttpPost]
        public ActionResult Demo(FormCollection formCollection)
        {
            try
            {
                var resultlist = new List<EmployeeExcel>();
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["importFile"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        using (var package = new ExcelPackage(file.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 1].Value.ToString() != "")
                                {
                                    var user = new EmployeeExcel();
                                    user.EmployeeName = workSheet.Cells[rowIterator, 1].Value.ToString();
                                    user.Email = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    user.EmployeeCode = workSheet.Cells[rowIterator, 3].Value.ToString();

                                    resultlist.Add(user);
                                }
                            }
                        }

                    }
                    var result = applicationRepository.SaveEmployee(resultlist);
                    if (result == 1)
                    {
                        TempData["Message"] = "Employee uploaded successfully";

                        return RedirectToAction("Demo", "ExcelUpload");
                    }
                    else
                    {
                        TempData["Error"] = "Something went wrong";

                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WaringMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }



    }



   
}