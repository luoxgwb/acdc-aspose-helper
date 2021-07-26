using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class JsonData
    {
        public string content1 { get; set; }
        public string content2 { get; set; }
        public static void CreateFile(string documentPath, string dataInFile)
        {
            File.WriteAllText(documentPath, dataInFile);

        }

        internal static void SetLicense(License license)
        {
            license.SetLicense("C:/Users/spq3658/source/repos/WebApplication1/Aspose.Total.lic");
        }

        internal static void DeleteFiles(string documentPath)
        {
            if (File.Exists(documentPath))
            {
                File.Delete(documentPath);
            }

        }
    }  
}
