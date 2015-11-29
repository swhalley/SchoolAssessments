using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolPdf
{
    public class AssessmentReader
    {
        public static List<SchoolTestResult> parse(String text)
        {
            var result = new List<SchoolTestResult>();

            List<string> lines = ExtractLinesFromTextAndCleanItUp(text);

            while (lines.Count > 0 )
            {
                var assessment = new SchoolTestResult();
                List<string> record = new List<string>();

                ParseClassInformation(assessment, lines);

                if (assessment.AssessmentType == "Math")
                {
                    record = ExtractSingleMathRecord(lines);
                    ParseMathSchoolTestResults(assessment, record);

                    result.Add(assessment);
                }
                else if (assessment.AssessmentType == "Literacy")
                {
                    record = ExtractSingleLiteracyRecord(lines);
                    ParseLiteracySchoolTestResults(assessment, record); //SMELL This duplication with Math can probably be narrowed down to the number of lines for each record.

                    result.Add(assessment);
                }

                lines.RemoveRange(0, record.Count);
            }

            return result;
        }

        private static List<string> ExtractLinesFromTextAndCleanItUp(String text)
        {
            return text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
                .Select(x => x.Trim())
                .Where(x => !x.StartsWith("*") && !x.EndsWith("*") && !String.IsNullOrWhiteSpace(x))
                .ToList();
        }

        private static void ParseClassInformation(SchoolTestResult assessment, List<string> record)
        {
            string[] classInformation = record[0].Split(' ');

            assessment.AssessmentType = classInformation[1];
            assessment.SchoolType = classInformation[0];
            assessment.ClassName = classInformation[classInformation.Length - 1];
        }


        #region Math Extractor
        private static List<string> ExtractSingleMathRecord(List<string> lines)
        {
            return lines.Take(6).ToList();
        }


        private static void ParseMathSchoolTestResults(SchoolTestResult assessment, List<string> record)
        {
            string[] schoolTestDetails = record[4].Split(' ');
            string[] testYear = record[3].Split(' ');

            assessment.SchoolName = schoolTestDetails[0];
            assessment.NumberOfStudents = Int32.Parse(schoolTestDetails[1]);
            assessment.AverageScore = Int32.Parse(schoolTestDetails[2]);
            assessment.PercentagePassed = Int32.Parse(schoolTestDetails[3]);

            assessment.AssessmentYear = testYear[0];
        }

        #endregion MathExtractor

        #region Literacy Extractor
        private static List<string> ExtractSingleLiteracyRecord(List<string> lines)
        {
            return lines.Take(8).ToList();
        }

        private static void ParseLiteracySchoolTestResults(SchoolTestResult assessment, List<string> record)
        {
            string schoolNamePattern = 
                "^"             //Match from beginning of the string
                + ".*?"         //Any character, zero or more times
                + @"(?=\d+)";   //Until a number is hit


            Regex regex = new Regex(schoolNamePattern);
            Match match = regex.Match( record[6] );

            assessment.SchoolName = match.Value.Trim();

            MatchCollection scores = Regex.Matches(record[6], "[0-9]+");
            assessment.NumberOfStudents = Int32.Parse(scores[0].Value);
            assessment.AverageScore = Int32.Parse(scores[1].Value);
            assessment.PercentagePassed = Int32.Parse(scores[2].Value);

            string[] testYear = record[5].Split(' ');
            assessment.AssessmentYear = testYear[0];
        }
        #endregion Literacy Extractor
    }
}
