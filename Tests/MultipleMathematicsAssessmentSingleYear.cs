using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class MultipleMathematicsAssessmentSingleYear
    {

        String data = @"Mathematics Assessments*
Secondary Math Assessment  521A
Results
Number of Students Average Score % Passed

2015 2015 2015
Bluefield 95 58 68
ELSB 572 55 61

Secondary Math Assessment  521B
Results
Number of Students Average Score % Passed

2015 2015 2015
Bluefield 93 58 67
ELSB 578 61 71

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

        [TestMethod]
        public void ReturnsTwoRecords()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void CanReadTheSchoolName()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual("Bluefield", result[0].SchoolName);
            Assert.AreEqual("Bluefield", result[1].SchoolName);
        }

        [TestMethod]
        public void CanReadAssessmentType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual("Math", result[0].AssessmentType);
            Assert.AreEqual("Math", result[1].AssessmentType);

        }

        [TestMethod]
        public void CanReadNumberOfStudents()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(95, result[0].NumberOfStudents);
            Assert.AreEqual(93, result[1].NumberOfStudents);

        }

        [TestMethod]
        public void CanReadAverageScore()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(58, result[0].AverageScore);
            Assert.AreEqual(58, result[1].AverageScore);
        }

        [TestMethod]
        public void CanReadPassPercentage()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(68, result[0].MetExpectations);
            Assert.AreEqual(67, result[1].MetExpectations);

        }

        [TestMethod]
        public void CanReadSchoolType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);
            Assert.AreEqual("Secondary", result[0].SchoolType);
            Assert.AreEqual("Secondary", result[1].SchoolType);
        }

        [TestMethod]
        public void CanReadClassName()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);
            Assert.AreEqual("521A", result[0].ClassName);
            Assert.AreEqual("521B", result[1].ClassName);
        }

    }
}
