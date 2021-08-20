// $Date: 2021-07-16 10:15:28 +0300 (Пт, 16 июл 2021) $
// $Revision: 543 $
// $Author: eloginov $


using System;
using System.Xml;
namespace A0Tests.Runner
{
    class OutputResults
    {

        private const string failed = "failed";
        private const string name = "name";
        private const string fullname = "fullname";
        private const string result = "result";
        private const string asserts = "asserts";
        private const string passed = "passed";
        private const string errorMode = "Failed";
        private const string successMode = "Passed";

        public delegate void HandlerOutColor(string message, ConsoleColor color = ConsoleColor.White);
        public delegate void HandlerLog(string message);
        public delegate void HandlerOutSave(string message, bool Append);

        public event HandlerOutColor NotifyOut;
        public event HandlerOutSave NotifyOutSave;

        public event HandlerLog NotifyLog;
        public event HandlerLog NotifyLogSave;

        /// <summary>
        /// Чтение и печать результатов запуска тестов.
        /// </summary>
        /// <param name="ResFilePath"></param>
        public void PrintResults(string ResFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ResFilePath);

            //Форматирование xml файла.
            doc.CreateWhitespace("\n");
            doc.Save(ResFilePath);

            // Получаем корневой элемент документа.
            XmlElement root = doc.DocumentElement;
            //root.Save(ResFilePath, SaveOptions.None);

            NotifyLog?.Invoke("Output started"); //запись в лог

            // Используем методы для рекурсивного обхода документа.
            ParseMsgFromXML(root, resMode: errorMode); //вывод только тест-кейсов с ошибками
            ParseMsgFromXML(root, resMode: successMode); //вывод только успешно пройденных тест-кейсов

            NotifyLog?.Invoke("Output finished"); //запись в лог
            //NotifyLog?.Invoke("Results saved in " + .ResFilePath);
         
        }

        private void ParseMsgFromXML(XmlElement item, string resMode, int indent = 0 )
        {

            if (resMode == errorMode && item.LocalName == "test-suite" && item.GetAttribute("type") == "TestSuite")
            {
                NotifyOut?.Invoke("");
                NotifyOut?.Invoke((" Name of test-suite: " + item.GetAttribute(name)), ConsoleColor.Yellow);
                NotifyOut?.Invoke($"{new string('\t', indent)} Total count of tests: {item.GetAttribute("total")} ");

                string attributePassed = item.GetAttribute(passed);
                if (attributePassed != "0")
                {
                    NotifyOut?.Invoke($"{new string('\t', indent)} Count of passed tests: {attributePassed} ", ConsoleColor.Green);
                }
                else
                {
                    NotifyOut?.Invoke($"{new string('\t', indent)} Count of passed tests: {attributePassed} ");
                }
                string attributeFailed = item.GetAttribute(failed);
                if (attributeFailed != "0")  //Есть ошибки в test-suite
                {
                    NotifyOut?.Invoke($"{new string('\t', indent)} Count of failed tests: {attributeFailed} ", ConsoleColor.Red);
                }
                else
                {
                    NotifyOut?.Invoke($"{new string('\t', indent)} Count of failed tests: {attributeFailed} ");
                }
                NotifyOut?.Invoke($"{new string('\t', indent)} Duration (ms): {item.GetAttribute("duration")} ");
            }

            else if (item.LocalName == "test-case" && item.GetAttribute(result) == resMode) 
            {
                NotifyOut?.Invoke("");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Name of test-case: {item.GetAttribute(name)}");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} full name: {item.GetAttribute(fullname)} ");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Class name: {item.GetAttribute("classname")} ");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Method name: {item.GetAttribute("methodname")} ");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Duration (ms): {item.GetAttribute("duration")} ");

                string attributeResult = item.GetAttribute(result);
                if (attributeResult == "Failed") //Есть ошибки в test-case
                {
                    NotifyOut?.Invoke($"{new string('\t', indent = 1)} Result: {attributeResult} ", ConsoleColor.Red);
                }
                else
                {
                    NotifyOut?.Invoke($"{new string('\t', indent = 1)} Result: {attributeResult} ", ConsoleColor.Green);
                }


                if (resMode == errorMode && item.SelectSingleNode("failure/message") != null) //есть сообщение об ошибки
                {
                    // элемент есть
                    XmlNodeList elemList = item.GetElementsByTagName("message"); //вывод ошибки
                    NotifyOut?.Invoke($"{new string('\t', indent = 1)} Error: {new string('\t', indent = 1)} {elemList[0].InnerText} ");
                }

                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Count of asserts: {item.GetAttribute(asserts)} ");
                NotifyOut?.Invoke($"{new string('\t', indent = 1)} Runstate: {item.GetAttribute("runstate")} ");
                NotifyOut?.Invoke("");
            }

            //(Имеется дочерний элемент)
            foreach (var child in item.ChildNodes)
            {
                if (child is XmlElement node)
                {
                    ParseMsgFromXML(node, resMode);
                }
            }
        }
    }
}
