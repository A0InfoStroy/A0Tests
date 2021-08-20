using System;
using System.Xml;

namespace A0Tests.Runner
{
    class SaveToXml : ISave
    {
        private readonly string filePath;
        public SaveToXml(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException("filePath must be initialized");

            if (filePath == "")
            {
                throw new ArgumentException("filePath is empty");
            }
        }

        public void Save(ITestsResult iTestResult)
        {
            const string totalCount = "totalcount";
            const string countOfPassed = "count-of-passed";
            const string countOfFailed = "count-of-failed";
            const string countOfTimeOut = "count-of-timeout";
            const string totalTime = "totaltime";

            const string testCase = "test-case";
            const string name = "name";
            const string fullName = "fullname";
            const string result = "result";
            const string duration = "duration";

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("tests");

            XmlAttribute attributeTotalCount = xmlDoc.CreateAttribute(totalCount);
            XmlAttribute attributeCountOfPassed = xmlDoc.CreateAttribute(countOfPassed);
            XmlAttribute attributeCountOfFailed = xmlDoc.CreateAttribute(countOfFailed);
            XmlAttribute attributeCountOfTimeOut = xmlDoc.CreateAttribute(countOfTimeOut);
            XmlAttribute attributeTotalTime = xmlDoc.CreateAttribute(totalTime);

            attributeTotalCount.Value = iTestResult.CountOfAll.ToString();
            attributeCountOfPassed.Value = iTestResult.CountOfPassed.ToString();
            attributeCountOfFailed.Value = iTestResult.CountOfFailed.ToString();
            attributeCountOfTimeOut.Value = iTestResult.CountOfTimeOut.ToString();
            attributeTotalTime.Value = iTestResult.TotalTime.ToString();

            rootNode.Attributes.Append(attributeTotalCount);
            rootNode.Attributes.Append(attributeCountOfPassed);
            rootNode.Attributes.Append(attributeCountOfFailed);
            rootNode.Attributes.Append(attributeCountOfTimeOut);
            rootNode.Attributes.Append(attributeTotalTime);

            xmlDoc.AppendChild(rootNode);


            foreach (ResultInfo test in iTestResult.GetTimeOut())
            {
                XmlNode testNode = xmlDoc.CreateElement(testCase);
                XmlAttribute attributeName = xmlDoc.CreateAttribute(name);
                XmlAttribute attributeFullName = xmlDoc.CreateAttribute(fullName);
                XmlAttribute attributeRes = xmlDoc.CreateAttribute(result);
                XmlAttribute attributeDuration = xmlDoc.CreateAttribute(duration);

                attributeName.Value = test.Name;
                attributeFullName.Value = test.Path;
                attributeRes.Value = test.Result.ToString();
                attributeDuration.Value = test.Duration.ToString();

                testNode.Attributes.Append(attributeName);
                testNode.Attributes.Append(attributeFullName);
                testNode.Attributes.Append(attributeRes);
                testNode.Attributes.Append(attributeDuration);

                rootNode.AppendChild(testNode);

                XmlNode messageNode = xmlDoc.CreateElement("failure");
                testNode.AppendChild(messageNode);

                XmlCDataSection cdata = xmlDoc.CreateCDataSection(test.ErrorMsg);

                messageNode.AppendChild(cdata);
            }

            foreach (ResultInfo test in iTestResult.GetFailed())
            {
                XmlNode testNode = xmlDoc.CreateElement(testCase);
                XmlAttribute attributeName = xmlDoc.CreateAttribute(name);
                XmlAttribute attributeFullName = xmlDoc.CreateAttribute(fullName);
                XmlAttribute attributeRes = xmlDoc.CreateAttribute(result);
                XmlAttribute attributeDuration = xmlDoc.CreateAttribute(duration);

                attributeName.Value = test.Name;
                attributeFullName.Value = test.Path;
                attributeRes.Value = test.Result.ToString();
                attributeDuration.Value = test.Duration.ToString();

                testNode.Attributes.Append(attributeName);
                testNode.Attributes.Append(attributeFullName);
                testNode.Attributes.Append(attributeRes);
                testNode.Attributes.Append(attributeDuration);

                rootNode.AppendChild(testNode);

                XmlNode messageNode = xmlDoc.CreateElement("failure");
                testNode.AppendChild(messageNode);

                XmlCDataSection cdata = xmlDoc.CreateCDataSection(test.ErrorMsg);

                messageNode.AppendChild(cdata);
            }

            foreach (ResultInfo test in iTestResult.GetPassed())
            {
                XmlNode testNode = xmlDoc.CreateElement(testCase);
                XmlAttribute attributeName = xmlDoc.CreateAttribute(name);
                XmlAttribute attributeFullName = xmlDoc.CreateAttribute(fullName);
                XmlAttribute attributeRes = xmlDoc.CreateAttribute(result);
                XmlAttribute attributeDuration = xmlDoc.CreateAttribute(duration);

                attributeName.Value = test.Name;
                attributeFullName.Value = test.Path;
                attributeRes.Value = test.Result.ToString();
                attributeDuration.Value = test.Duration.ToString();

                testNode.Attributes.Append(attributeName);
                testNode.Attributes.Append(attributeFullName);
                testNode.Attributes.Append(attributeRes);
                testNode.Attributes.Append(attributeDuration);

                rootNode.AppendChild(testNode);
            }

            xmlDoc.CreateWhitespace("\n");
            xmlDoc.Save(filePath);
        }
    }
}
