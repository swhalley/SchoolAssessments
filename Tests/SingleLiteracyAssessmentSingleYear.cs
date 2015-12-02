using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class SingleLiteracyAssessmentSingleYear
    {
        String text = @"Elementary Literacy Assessment
Results for Reading Comprehension and Writing
 Reading Comprehension Writing
Number of Students
% Met Expectations  % Met Expectations
2014 2014 2014
École La-Belle-Cloche 4 90 78
CSLF 72 83 51";
        //TODO HANDLE -- results when < 10 kids

        [TestMethod]
        public void ReturnsAListOfObjects()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual(1, result.Count);
        }


        [TestMethod]
        public void CanReadTheSchoolName()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual("École La-Belle-Cloche", result[0].SchoolName);
        }

        [TestMethod]
        public void CanReadAssessmentType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual("Literacy", result[0].AssessmentType);
        }

        [TestMethod]
        public void CanReadNumberOfStudents()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual(4, result[0].NumberOfStudents);
        }

        [TestMethod]
        public void CanReadAverageScore()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual(90, result[0].AverageScore);
        }

        [TestMethod]
        public void CanReadPassPercentage()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);

            Assert.AreEqual(78, result[0].PercentagePassed);
        }

        [TestMethod]
        public void CanReadSchoolType()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);
            Assert.AreEqual("Elementary", result[0].SchoolType);
        }

        [TestMethod]
        public void CanReadClassName()
        {
            //TODO Some schools don't have class names...GAH DATA
            //Since there are no way to tell this from a string line, we will manually have to clean up data generated
        }

        [TestMethod]
        public void CanReadAssessmentYear()
        {
            List<SchoolTestResult> result = new AssessmentReader().parse(text);
            Assert.AreEqual("2014", result[0].AssessmentYear);
        }

    }
}
