using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class MultipleVariousAssessmentTypesSingleYear
    {

        String data = @"Mathematics Assessments*
Secondary Math Assessment  521A
Results
Number of Students Average Score % Passed

2015 2015 2015
Bluefield 95 58 68
ELSB 572 55 61

Elementary Literacy Assessment
Results for Reading Comprehension and Writing
 Reading Comprehension Writing
Number of Students
% Met Expectations  % Met Expectations

2014 2014 2014
École La-Belle-Cloche 4 90 78
CSLF 72 83 51

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
            Assert.AreEqual("École La-Belle-Cloche", result[1].SchoolName);
        }

        [TestMethod]
        public void CanReadAssessmentType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual("Math", result[0].AssessmentType);
            Assert.AreEqual("Literacy", result[1].AssessmentType);

        }

        [TestMethod]
        public void CanReadNumberOfStudents()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(95, result[0].NumberOfStudents);
            Assert.AreEqual(4, result[1].NumberOfStudents);

        }

        [TestMethod]
        public void CanReadAverageScore()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(58, result[0].AverageScore);
            Assert.AreEqual(90, result[1].AverageScore);
        }

        [TestMethod]
        public void CanReadPassPercentage()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);

            Assert.AreEqual(68, result[0].PercentagePassed);
            Assert.AreEqual(78, result[1].PercentagePassed);

        }

        [TestMethod]
        public void CanReadSchoolType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);
            Assert.AreEqual("Secondary", result[0].SchoolType);
            Assert.AreEqual("Elementary", result[1].SchoolType);
        }

        [TestMethod]
        public void CanReadClassName()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(data);
            Assert.AreEqual("521A", result[0].ClassName);
            //Not checking Elementary school because it isn't listed
        }

    }
}
