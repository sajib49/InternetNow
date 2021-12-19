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
            var fileFolder = HttpContext.Current.Server.MapPath("~/Files/");

            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }
            string filePath = Path.Combine(fileFolder, "INow.txt");

            if (!(File.Exists(filePath)))
            {
                File.CreateText(filePath);
            }

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

        [NonAction]
        private bool CheckDataValidation(string filename,List<string> dataList, FileInputViewModel fileDataDetails)
        {
            var fileSizeInKb = new FileInfo(filename).Length / 1024;
            if (fileSizeInKb >= fileDataDetails.FileSettings.FileSizeInKb)
            {
                return false;
            }

            if (fileDataDetails.FileSettings.NumericDataPercentage != null
                || fileDataDetails.FileSettings.AlphanumericDataPercentage != null
                || fileDataDetails.FileSettings.FloatDataPercentage != null)
            {
                int numericDataOccurence = 0;
                int alphanumericDataOccurence = 0;
                int floatDataOccurence = 0;

                foreach (var aData in dataList)
                {
                    float f;
                    int i;
                    if (float.TryParse(aData, out f))
                    {
                        ++floatDataOccurence;
                    }
                    else if (int.TryParse(aData, out i))
                    {
                        ++numericDataOccurence;
                    }
                    else
                    {
                        ++alphanumericDataOccurence;
                    }
                }

                int numericDataOccurenceInPercentage = numericDataOccurence>0 ? ((numericDataOccurence/dataList.Count)*100) : 0;
                int alphanumericDataOccurenceInPercentage = alphanumericDataOccurence > 0 ? ((alphanumericDataOccurence / dataList.Count) * 100) : 0;
                int floatDataOccurenceInPercentage = floatDataOccurence > 0 ? ((floatDataOccurence / dataList.Count) * 100) : 0;

                if (fileDataDetails.FileSettings.NumericDataPercentage > 0 &&
                    numericDataOccurenceInPercentage >= fileDataDetails.FileSettings.NumericDataPercentage)
                {
                    return false;
                }

                if (fileDataDetails.FileSettings.AlphanumericDataPercentage > 0 &&
                    alphanumericDataOccurenceInPercentage >= fileDataDetails.FileSettings.AlphanumericDataPercentage)
                {
                    return false;
                }

                if (fileDataDetails.FileSettings.FloatDataPercentage > 0 &&
                    floatDataOccurenceInPercentage >= fileDataDetails.FileSettings.FloatDataPercentage)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
