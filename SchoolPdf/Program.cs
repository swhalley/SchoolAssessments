using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            var simple = "http://www.gov.pe.ca/eecd/assessments/Bluefield-High.pdf";
            var complex = "http://www.gov.pe.ca/eecd/assessments/Ecole%20La-Belle-Cloche%202012_2014_CSLF_English.pdf";
            Uri uri = new Uri(complex);
            PdfReader reader = new PdfReader(uri);

            String text = PdfTextExtractor.GetTextFromPage(reader, 1);

            //List<SchoolTestResult> result = AssessmentReader.parse(text);
            //string output = JsonConvert.SerializeObject(result, Formatting.Indented);
            //Console.WriteLine(output);

            Console.WriteLine(text);
            Console.ReadLine();
        }
    }
}
