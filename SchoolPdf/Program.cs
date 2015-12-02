using FileHelpers;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SchoolTestResult> result = new List<SchoolTestResult>();

            string[] urls = ConfigurationManager.AppSettings["urls"].Split(',');
            Console.WriteLine("Reading " + urls.Length + " files");

            foreach (string url in urls)
            {
                Uri uri = new Uri(url);
                PdfReader reader = new PdfReader(uri);

                try
                {
                    String text = PdfTextExtractor.GetTextFromPage(reader, 1);
                    result.AddRange(new AssessmentReader().parse(text));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Reading " + url);
                    Console.WriteLine(e);
                }
            }
            
            string jsonOutput = JsonConvert.SerializeObject(result, Formatting.Indented);
            string csvOutput = new FileHelperEngine<SchoolTestResult>().WriteString( result );

            Console.WriteLine(jsonOutput);
            Console.WriteLine(csvOutput);


            //Console.WriteLine(text);
            Console.ReadLine();
        }
    }
}
