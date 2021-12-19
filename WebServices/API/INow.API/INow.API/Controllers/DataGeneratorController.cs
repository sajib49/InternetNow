using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using INow.API.Models;
using INow.API.Models.ViewModel;

namespace INow.API.Controllers
{
    [RoutePrefix("api/data-generator")]
    public class DataGeneratorController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(FileInput))]
        [Route("save-data")]
        public IHttpActionResult SaveRandomTextOrNumber([FromBody] FileInputViewModel fileDataDetails)
        {
            string filePath = GetFilePath();

            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                line = sr.ReadLine();
            }

            var dataList = line?.Split(',').Take(20).ToList();

            using (StreamWriter sw = (File.Exists(filePath)) ? File.AppendText(filePath) : File.CreateText(filePath))
            {
                if (CheckDataValidation(filePath, dataList, fileDataDetails))
                {
                    string s = string.Empty;
                    if (new FileInfo(filePath).Length != 0)
                    {
                        s += ",";
                    }
                    s += string.Format("{0},{1},{2}", fileDataDetails.FileInputs.NumericInput, fileDataDetails.FileInputs.AlphanumericInput, fileDataDetails.FileInputs.FloatInput);
                    sw.Write(s);
                    return Ok(fileDataDetails.FileInputs);
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));
            }
        }


        [HttpGet]
        [ResponseType(typeof (DataPercentage))]
        [Route("get-data")]
        public IHttpActionResult GetDataForRepost()
        {
            return Ok(GetDataPercentage());
        }

        [NonAction]
        private string GetFilePath()
        {
            var fileFolder = HttpContext.Current.Server.MapPath("~/Files/");

            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }
            string filePath = Path.Combine(fileFolder, Constants.FileName);

            if (!(File.Exists(filePath)))
            {
                File.CreateText(filePath);
            }
            return filePath;
        }

        [NonAction]
        private DataPercentage GetDataPercentage()
        {
            string filePath = GetFilePath();

            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                line = sr.ReadLine();
            }
            var dataList = line?.Split(',').Take(20).ToList();

            int numericDataOccurence = 0;
            int alphanumericDataOccurence = 0;
            int floatDataOccurence = 0;

            if (dataList != null)
            {
                foreach (var aData in dataList)
                {
                    float f;
                    int i;
                    if (int.TryParse(aData, out i))
                    {
                        ++numericDataOccurence;
                    }
                    else if (float.TryParse(aData, out f))
                    {
                        ++floatDataOccurence;
                    }
                    else
                    {
                        ++alphanumericDataOccurence;
                    }
                }

                return new DataPercentage
                {
                    AlphanumericDataPercentage = alphanumericDataOccurence > 0 ? (int)Math.Round((double)(100 * alphanumericDataOccurence) / dataList.Count) : 0,
                    FloatDataPercentage = floatDataOccurence > 0 ? (int) Math.Round((double)(100 * floatDataOccurence) / dataList.Count) : 0,
                    NumericDataPercentage = numericDataOccurence > 0 ? (int) Math.Round((double)(100 * numericDataOccurence) / dataList.Count) : 0
                };
            }
            return null;
        }

        [NonAction]
        private bool CheckDataValidation(string filename,List<string> dataList, FileInputViewModel fileDataDetails)
        {
            var fileSizeInKb = new FileInfo(filename).Length / 1024;
            if (fileSizeInKb >= fileDataDetails.FileSettings.FileSizeInKb)
            {
                return false;
            }
            var dataPercentage = GetDataPercentage();

            if (fileDataDetails.FileSettings.NumericDataPercentage > 0 &&
                   dataPercentage.NumericDataPercentage >= fileDataDetails.FileSettings.NumericDataPercentage)
            {
                return false;
            }

            if (fileDataDetails.FileSettings.AlphanumericDataPercentage > 0 &&
                dataPercentage.AlphanumericDataPercentage >= fileDataDetails.FileSettings.AlphanumericDataPercentage)
            {
                return false;
            }

            if (fileDataDetails.FileSettings.FloatDataPercentage > 0 &&
                dataPercentage.FloatDataPercentage >= fileDataDetails.FileSettings.FloatDataPercentage)
            {
                return false;
            }

            return true;
        }
    }
}
