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
        private string scoresPattern = @"([0-9]+|\-{2})"; //Matches any number or double dash (--)
        private string schoolNamePattern =
                "^"             //Match from beginning of the string
                + ".*?"         //Any character, zero or more times
                + @"(?=\d+)";   //Until a number is hit

        public List<SchoolTestResult> parse(String text)
        {
            var result = new List<SchoolTestResult>();

            List<string> lines = ExtractLinesFromTextAndCleanItUp(text);

            while (lines.Count > 0)
            {
                List<string> record = new List<string>();

                string assessmentType = getAssessmentType(lines);
                int recordSize = (assessmentType == "Math") ? 6 : 8;

                record = ExtractSingleRecord(lines, recordSize);

                int assessmentYears = getNumberOfAssessmentYears(record, recordSize);

                for (int yearIndex = 1; yearIndex <= assessmentYears; yearIndex++)
                {
                    var assessment = new SchoolTestResult();
                    ParseClassInformation(assessment, lines);
                    ParseSchoolName(assessment, record, recordSize);
                    ParseTestScores(assessment, record, recordSize, yearIndex);
                    result.Add(assessment);
                }

                lines.RemoveRange(0, recordSize);
            }

            return result;
        }

        private List<string> ExtractLinesFromTextAndCleanItUp(String text)
        {
            return text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None)
                .Select(x => x.Trim())
                .Where(x => !x.StartsWith("*") && !x.EndsWith("*") && !String.IsNullOrWhiteSpace(x))
                .ToList();
        }

        private string getAssessmentType(List<string> lines)
        {
            return lines[0].Split(' ')[1];
        }

        private int getNumberOfAssessmentYears(List<string> record, int recordLineSize)
        {
            return (Regex.Matches(record[recordLineSize - 2], scoresPattern).Count) / 3;
        }

        private List<string> ExtractSingleRecord(List<string> lines, int recordLineSize)
        {
            return lines.Take(recordLineSize).ToList();
        }

        private void ParseClassInformation(SchoolTestResult assessment, List<string> record)
        {
            string[] classInformation = record[0].Split(' ');

            assessment.AssessmentType = classInformation[1];
            assessment.SchoolType = classInformation[0];
            assessment.ClassName = classInformation[classInformation.Length - 1];
        }

        private void ParseSchoolName(SchoolTestResult assessment, List<string> record, int recordLineSize)
        {
            Regex regex = new Regex(schoolNamePattern);
            Match match = regex.Match(record[recordLineSize - 2]);

            assessment.SchoolName = match.Value.Trim();
        }

        private void ParseTestScores(SchoolTestResult assessment, List<string> record, int recordLineSize, int yearOffset)
        {
            MatchCollection scores = Regex.Matches(record[recordLineSize - 2], scoresPattern);

            assessment.NumberOfStudents = Int32.Parse(scores[0].Value);

            int number;
            Int32.TryParse(scores[1].Value, out number);
            assessment.AverageScore = number;

            Int32.TryParse(scores[2].Value, out number);
            assessment.PercentagePassed = number;



            string[] testYear = record[recordLineSize - 3].Split(' ');
            assessment.AssessmentYear = testYear[0];
        }
    }
}
