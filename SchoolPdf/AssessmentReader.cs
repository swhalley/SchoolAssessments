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
                int recordSize = (assessment.AssessmentType == "Math") ? 6 : 8;
                
                record = ExtractSingleRecord(lines, recordSize);
                ParseSchoolTestResults(assessment, record, recordSize);
                result.Add(assessment);

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

        private static List<string> ExtractSingleRecord(List<string> lines, int recordLineSize)
        {
            return lines.Take(recordLineSize).ToList();
        }

        private static void ParseSchoolTestResults(SchoolTestResult assessment, List<string> record, int recordLineSize)
        {
            string schoolNamePattern = 
                "^"             //Match from beginning of the string
                + ".*?"         //Any character, zero or more times
                + @"(?=\d+)";   //Until a number is hit

            string scoresPattern = @"([0-9]+|\-{2})"; //Matches any number or double dash (--)


            Regex regex = new Regex(schoolNamePattern);
            Match match = regex.Match( record[recordLineSize-2] );

            assessment.SchoolName = match.Value.Trim();

            MatchCollection scores = Regex.Matches(record[recordLineSize - 2], scoresPattern);

            assessment.NumberOfStudents = Int32.Parse(scores[0].Value);

            int number;
            Int32.TryParse(scores[1].Value, out number);
            assessment.AverageScore = number;

            Int32.TryParse(scores[2].Value, out number);
            assessment.PercentagePassed = number;

            string[] testYear = record[recordLineSize-3].Split(' ');
            assessment.AssessmentYear = testYear[0];
        }
    }
}
