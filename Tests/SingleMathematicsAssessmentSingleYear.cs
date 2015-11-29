using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class SingleMathematicsAssessmentSingleYear
    {
        String text = @"Mathematics Assessments*
Secondary Math Assessment  521A
Results
Number of Students Average Score % Passed

2015 2015 2015
Bluefield 95 58 68
ELSB 572 55 61

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

        [TestMethod]
        public void CanReadTheSchoolName()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);

            Assert.AreEqual("Bluefield", result[0].SchoolName);
        }

        [TestMethod]
        public void CanReadAssessmentType()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);

            Assert.AreEqual("Math", result[0].AssessmentType);
        }

        [TestMethod]
        public void CanReadNumberOfStudents()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);

            Assert.AreEqual(95, result[0].NumberOfStudents);
        }

        [TestMethod]
        public void CanReadAverageScore()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);

            Assert.AreEqual(58, result[0].AverageScore);
        }

        [TestMethod]
        public void CanReadPassPercentage()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);

            Assert.AreEqual(68, result[0].PercentagePassed);
        }

        [TestMethod]
        public void CanReadSchoolType()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);
            Assert.AreEqual("Secondary", result[0].SchoolType);
        }
        
        [TestMethod]
        public void CanReadClassName()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);
            Assert.AreEqual("521A", result[0].ClassName);
        }

        [TestMethod]
        public void CanReadAssessmentYear()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(text);
            Assert.AreEqual("2015", result[0].AssessmentYear);
        }

    }
}
