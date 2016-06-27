using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HapCss.UnitTests
{
    [TestClass]
    public class Html1
    {
        static HtmlAgilityPack.HtmlDocument doc = LoadHtml();

        [TestMethod]
        public void IdSelectorMustReturnOnlyFirstElement()
        {
            var elements = doc.QuerySelectorAll("#myDiv");

            Assert.IsTrue(elements.Count == 1);
            Assert.IsTrue(elements[0].Id == "myDiv");
            Assert.IsTrue(elements[0].Attributes["first"].Value == "1");
        }

        [TestMethod]
        public void GetElementsByAttribute()
        {
            var elements = doc.QuerySelectorAll("*[id=myDiv]");

            Assert.IsTrue(elements.Distinct().Count() == 2 && elements.Count == 2);
            for (int i = 0; i < elements.Count; i++)
                Assert.IsTrue(elements[i].Id == "myDiv");
        }

        [TestMethod]
        public void GetElementsByClassName1()
        {
            var elements1 = doc.QuerySelectorAll(".cls-a");
            var elements2 = doc.QuerySelectorAll(".clsb");

            Assert.IsTrue(elements1.Count == 1);
            for (int i = 0; i < elements1.Count; i++)
                Assert.IsTrue(elements1[i] == elements2[i]);
        }

		[TestMethod]
        public void GetElementsByClassName_MultiClasses()
        {
            var elements = doc.QuerySelectorAll(".cls-a, .cls-b");

            Assert.IsTrue(elements.Count == 2);
            Assert.IsTrue(elements[0].Id == "spanA");
            Assert.IsTrue(elements[1].Id == "spanB");
        }

		[TestMethod]
        public void GetElementsByClassName_WithUnderscore()
        {
            var elements = doc.QuerySelectorAll(".underscore_class");

            Assert.IsTrue(elements.Count == 1);
            Assert.IsTrue(elements[0].Id == "spanB");
        }



        private static HtmlAgilityPack.HtmlDocument LoadHtml()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(Resource.GetString("Test1.html"));

            return doc;
        }

    }
}
