using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPdf;

namespace Tests
{
    [TestClass]
    public class GrabBag
    {
        [TestMethod]
        public void ParseMathSchoolNameWhenMultipleWords()
        {
            string text = @"Mathematics Assessments*
Primary Math Assessment
Results
Number of Students Average Score % Met Expectations

2012 2012 2012
École La-Belle-Cloche 7  56 56
CSLF 61 73 68

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

            Assert.AreEqual("École La-Belle-Cloche", AssessmentReader.parse(text)[0].SchoolName);

        }

        [TestMethod]
        public void SupportClassSizeThatIsTooSmallToShowAverageScore()
        {
            string text = @"Mathematics Assessments*
Primary Math Assessment
Results
Number of Students Average Score % Met Expectations

2012 2012 2012
École La-Belle-Cloche 7  -- --
CSLF 61 73 68

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

            Assert.AreEqual(0, AssessmentReader.parse(text)[0].AverageScore);

        }


        [TestMethod]
        public void SupportClassSizeThatIsTooSmallToShowPercentagePassed()
        {
            string text = @"Mathematics Assessments*
Primary Math Assessment
Results
Number of Students Average Score % Met Expectations

2012 2012 2012
École La-Belle-Cloche 7  -- --
CSLF 61 73 68

*To protect the privacy of students, results of schools with ten or fewer students are not reported publically.";

            Assert.AreEqual(0, AssessmentReader.parse(text)[0].PercentagePassed);

        }
    }
}
