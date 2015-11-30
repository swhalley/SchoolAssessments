using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class MultieYearRecord
    {
        string twoYears = @"Mathematics Assessments*
Primary Math Assessment
Results
Number of Students Average Score % Met Expectations

2012 2013 2012 2013 2012 2013
École La-Belle-Cloche 7 10 45 64 23 98
CSLF 61 73 68 81 82 78 80 89 73

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

        string threeYears = @"Mathematics Assessments*
Primary Math Assessment
Results
Number of Students Average Score % Met Expectations

2012 2013 2014 2012 2013 2014 2012 2013 2014
École La-Belle-Cloche 7 10 4 46 65 85 12 45 68
CSLF 61 73 68 81 82 78 80 89 73

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

        [TestMethod]
        public void ReadsTwoYears()
        {
            List<SchoolTestResult> result = AssessmentReader.parse(twoYears);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(2012, result[0].AssessmentYear);
            Assert.AreEqual(7, result[0].NumberOfStudents);
            Assert.AreEqual(45, result[0].AverageScore);
            Assert.AreEqual(23, result[0].PercentagePassed);

            Assert.AreEqual(2013, result[1].AssessmentYear);
            Assert.AreEqual(10, result[1].NumberOfStudents);
            Assert.AreEqual(64, result[1].AverageScore);
            Assert.AreEqual(98, result[1].PercentagePassed);
        }
    }
}
