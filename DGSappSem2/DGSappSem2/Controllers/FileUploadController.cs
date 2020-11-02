using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DGSappSem2.business;
using DGSappSem2.Models;
using DGSappSem2.Models.FileUpload;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Net;

namespace PlayGroundMVC.Controllers
{
    public class FileUploadController : Controller
    {
        //List<FileUpload> _List = new List<FileUpload>();

        ////public bool UploadEnabled { get; set; }

        //private ApplicationDbContext db = new ApplicationDbContext();
        //// GET: FileUploadVm
        //public ActionResult Index()
        //{
        //    List<FileUploadModel> fileUp = db.fileUploadModel.ToList();
        //    return View(fileUp);
        //}
        //public ActionResult FileUploadProcess()
        //{
        //    var model = new FileUploadVm();
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult FileUploadProcess(FileUploadVm model, FileUploadBusiness fileUploadBusiness)
        //{
        //    fileUploadBusiness.UploadFile(model);
        //    return RedirectToAction("Index");
        //}
        //public FileContentResult FileDownload(int? id, FileUploadBusiness fileUploadBusiness)
        //{
        //    var file = fileUploadBusiness.SearchFile(id);
        //    return File(fileUploadBusiness.fileData(file), "text", fileUploadBusiness.fileName(file));
        //}

        //public ActionResult Upload()
        //{
        //    return View("Index");
        //}

        //public bool SetUploadEnabled(FileUploadVm model)
        //{
        //    return model.FileName != string.Empty;
        //}
        //string conString = "Data Source=.;Initial Catalog =DemoTest; integrated security=true;";

        //string conString = "Data Source=.;Initial Catalog =DemoTest; integrated security=true;";

        public int count = 1;
        System.Guid guid = System.Guid.NewGuid();

        // GET: Files  
        public ActionResult Index(FileUpload model)
        {
            var dtFiles = GetFileDetails();
            
            List<FileUpload> list = new List<FileUpload>();
            foreach (var item in dtFiles)
            {
                list.Add(new FileUpload
                {
                    //FileId = item.FileId,
                    FileId = (count++).ToString(),
                    FileName = item.FileName,
                    FileUrl = item.FileUrl
                });
            }
            model.FileList = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            FileUpload model = new FileUpload { 
            FileList = new List<FileUpload>()};
            FileUpload file;
            var dtFiles = GetFileDetails();
            foreach (var item in dtFiles)
            {
                file = new FileUpload
                {
                    FileId = item.FileId,
                    FileName = item.FileName,
                    FileUrl = item.FileUrl
                };
                model.FileList.Add(file);
            }

            if (files != null)
            {
                //var Extension = Path.GetExtension(files.FileName);
                //var fileName = "my-file-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), files.FileName);
                model.FileUrl = Url.Content(Path.Combine("~/UploadedFiles/", files.FileName));
                model.FileName = files.FileName;

                if (SaveFile(model))
                {
                    files.SaveAs(path);
                    TempData["AlertMessage"] = "Uploaded Successfully !!";
                    return RedirectToAction("Index", "FileUpload");
                }
                else
                {
                    ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                return View(model);
            }
            return RedirectToAction("Index", "FileUpload");
        }

        private List<FileUpload> GetFileDetails()
        {
            //DataTable dtData = new DataTable();
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand command = new SqlCommand("Select * From tblFileDetails", con);
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(dtData);
            //con.Close();
            //return dtData;
            var response = new List<FileUpload>();
           
            var path = @"E:\work\campus work\3rd year 2020\Project work\sem 2\project code\DGSrepo1\DGSappSem2\DGSappSem2\DGSappSem2\UploadedFiles";

            var test = Directory.GetFiles(path);
            foreach (var entry in test)
            {
                var file = new FileUpload {
               
                FileId = entry,
                FileName =Path.GetFileName(entry),
                FileUrl= entry
              
            };
                guid.ToString();
                response.Add(file);
            }

            return response;
        }

        private bool SaveFile(FileUpload model)
        {
            //string strQry = "INSERT INTO tblFileDetails (FileName,FileUrl) VALUES('" +
            //    model.FileName + "','" + model.FileUrl + "')";
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            //SqlCommand command = new SqlCommand(strQry, con);
            //int numResult = command.ExecuteNonQuery();
            //con.Close();
            if (model != null)

            {
                model.FileList.Add(model);
                guid.ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
        //public string filePath = @"E:\work\campus work\3rd year 2020\Project work\sem 2\project code\DGSrepo1\DGSappSem2\DGSappSem2\DGSappSem2\Files\DownloadFiles";
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);


        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileUpload assessment = await db.FileUpload.FindAsync(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FileUpload assessment = await db.FileUpload.FindAsync(id);
            db.Assessments.Remove(assessment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         



    }
}
