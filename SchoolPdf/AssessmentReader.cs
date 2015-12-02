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

                int numberAssessmentYears = getNumberOfAssessmentYears(record, recordSize);

                for (int yearOffset = 0; yearOffset < numberAssessmentYears; yearOffset++)
                {
                    var assessment = new SchoolTestResult();

                    getClassInformation(assessment, lines);
                    getSchoolName(assessment, record, recordSize);
                    getTestYear(assessment, record, recordSize, yearOffset);
                    getTestScores(assessment, record, recordSize, yearOffset);

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
                .Where(x => !x.Equals("Assessments based on each unique curriculum; therefore, results from these programs cannot be compared."))
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

        private void getClassInformation(SchoolTestResult assessment, List<string> record)
        {
            string[] classInformation = record[0].Split(' ');

            assessment.AssessmentType = classInformation[1];
            assessment.SchoolType = classInformation[0];

            string[] classNameTokens = record[0].Split(new string[] { "Assessment" }, StringSplitOptions.None);
            assessment.ClassName = (classNameTokens.Length >= 2) ? classNameTokens[1].Trim() : "";
        }

        private void getSchoolName(SchoolTestResult assessment, List<string> record, int recordLineSize)
        {
            Regex regex = new Regex(schoolNamePattern);
            Match match = regex.Match(record[recordLineSize - 2]);

            assessment.SchoolName = match.Value.Trim();
        }

        private void getTestScores(SchoolTestResult assessment, List<string> record, int recordLineSize, int yearOffset)
        {
            MatchCollection scores = Regex.Matches(record[recordLineSize - 2], scoresPattern);

            assessment.NumberOfStudents = Int32.Parse(scores[0 + yearOffset].Value);

            int number;

            var scoreGroup = scores.Count / 3;
            Int32.TryParse(scores[scoreGroup * 1 + yearOffset].Value, out number);
            assessment.AverageScore = number;

            Int32.TryParse(scores[scoreGroup * 2 + yearOffset].Value, out number);
            assessment.MetExpectations = number;
        }

        private void getTestYear(SchoolTestResult assessment, List<string> record, int recordLineSize, int yearOffset)
        {
            string[] testYear = record[recordLineSize - 3].Split(' ');
            assessment.AssessmentYear = testYear[yearOffset];
        }
    }
}
