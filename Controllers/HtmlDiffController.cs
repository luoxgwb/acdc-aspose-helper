using Aspose.Html;
using Aspose.Words;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HtmlTableDiffController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] JsonData content)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            string nameFormat = DateTime.UtcNow.ToFileTimeUtc().ToString();
            Console.WriteLine("Date time = " + DateTime.UtcNow.ToFileTimeUtc().ToString());
            //JsonData.SetLicense(license);
            license.SetLicense(Directory.GetCurrentDirectory() + "\\Aspose.Total.lic");
            //license.SetLicense("/Aspose.Total.lic");



            string documentPath1 = Directory.GetCurrentDirectory() + $"\\1_{nameFormat}.html";
            string documentPath2 = Directory.GetCurrentDirectory() + $"\\2_{nameFormat}.html";
            string comparisonDocument = Directory.GetCurrentDirectory() + $"\\Output_{nameFormat}.html";
      
            JsonData.CreateFile(documentPath1, content.content1);
            JsonData.CreateFile(documentPath2, content.content2);

            int added = 0;
            int deleted = 0;

            // Load both documents in Aspose.Words
            Document doc1 = new Document(documentPath1);
            Document doc2 = new Document(documentPath2);
            Document docComp = new Document(documentPath1);

            DocumentBuilder builder = new DocumentBuilder(docComp);
            doc1.Compare(doc2, "a", DateTime.Now);

            foreach (Revision revision in doc1.Revisions)
            {

                switch (revision.RevisionType)
                {

                    case RevisionType.Insertion:
                        added++;
                        break;

                    case RevisionType.Deletion:
                        deleted++;
                        break;

                }

                Console.WriteLine(revision.RevisionType + ": " + revision.ParentNode);
            }

         
            doc1.Save(comparisonDocument, SaveFormat.Html);
            var text = "";
            using (var document = new HTMLDocument(comparisonDocument))
            {
                text = document.DocumentElement.InnerHTML;
            }
            Console.WriteLine("\n the difference =  " + text);

            //Console.WriteLine("The content = " + content);
            JsonData.DeleteFiles(documentPath1);
            JsonData.DeleteFiles(documentPath2);
            JsonData.DeleteFiles(comparisonDocument);
            return text;
        }
    }
}
