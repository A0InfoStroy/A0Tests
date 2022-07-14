// $Date: 2021-08-19 10:30:06 +0300 (Чт, 19 авг 2021) $
// $Revision: 565 $
// $Author: eloginov $


using NUnit.Engine;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace A0Tests.Runner
{
    /// <summary>
    /// Запуск тестов на выполнение.
    /// </summary>
    class AppRun : AppBase
    {
        private string pathProject; //тестируемый проект
        private Properties.RunProperties.Filters filter; //фильтр
        private bool paparmsOutput; //вывод параметров на консоль

        private ILog log; //Поле для вывода в протокол
        private IOut iOutput;
        private ITestsResult iTestResult;
        private ISave save;
        public AppRun(Properties.RunProperties props, ILog log,
            IOut iOutput, ITestsResult iTestResult, ISave save) : base(props, log)
        {
            this.pathProject = props.Project ?? throw new ArgumentNullException("Project must be initialized");
            this.filter = props.Filter ?? throw new ArgumentNullException("Filter must be initialized");
            this.paparmsOutput = props.PaparamsOutput;
            this.log = log ?? throw new ArgumentNullException("ILog must be initialized");
            this.iOutput = iOutput ?? throw new ArgumentNullException("IOut must be initialized");
            this.iTestResult = iTestResult ?? throw new ArgumentNullException("ITestsResult must be initialized");
            this.save = save ?? throw new ArgumentNullException("ISave must be initialized");

            if (filter.Count == 0)
            {
                throw new ArgumentException("Filter is empty!");
            }
        }

        public override string Run() 
        {
            base.Run();
            TestPackage package = new TestPackage(pathProject);
            package.AddSetting("WorkDirectory", Environment.CurrentDirectory);
            ITestEngine engine = TestEngineActivator.CreateInstance();
            ITestFilterService filterService = engine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();

            //добавление всех фильтров
            foreach (string f in filter)
            {
                builder.AddTest(f);
            }

            TestFilter testFilter = builder.GetFilter();

            using (ITestRunner runner = engine.GetRunner(package))
            {
                // запуск тестов
                log.Info("Running tests");

                XmlNode result = runner.Run(null, testFilter);
                
                result.RemoveChild(result.SelectSingleNode("command-line"));
                XmlNode filterInfo = result.SelectSingleNode("filter/test");

                result.RemoveChild(result.SelectSingleNode("filter"));

                log.Info("Tests done");
                ParseFromXmlNode(result);

                save.Save(iTestResult); 

                return "";
            }
        }

        /// <summary>
        /// Парсинг результатов и добавление тестов.
        /// </summary>
        private void ParseFromXmlNode(XmlNode result)
        {

            const string name = "@name";
            const string fullname = "@fullname";
            const string duration = "@duration";

            //Настройка формата для дробных чисел.
            NumberFormatInfo doubleFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };
            XmlNodeList tests = result.SelectNodes("//test-suite/test-case");  //Выбор всех тест-кейсов.

            Regex regexTimeOut = new Regex(@"^(Test exceeded Timeout value of \d+ms)"); //Регулярное выражения для определения тестов с превышенным лимитом.

            foreach (XmlNode test in tests)
            {
                switch (test.SelectSingleNode("@result").Value) // Получение результата текущего теста.
                {
                    case "Passed":
                        iOutput.AddPassed(Name: test.SelectSingleNode(name).Value, Path: test.SelectSingleNode(fullname).Value,
                            Duration: TimeSpan.FromSeconds( Convert.ToDouble(test.SelectSingleNode(duration).Value, doubleFormat)));
                        break;  
                    case "Failed":
                        string errorMsg = test.SelectSingleNode("failure/message").InnerText; //Сообщение с описанием ошибки.
                        // Проверка на то, является ли текущий тест непройденным из-за превышенного лимита по времени.
                        if (regexTimeOut.IsMatch(errorMsg)) 
                        {
                            iOutput.AddTimeOut(Name: test.SelectSingleNode(name).InnerText, Path: test.SelectSingleNode(fullname).Value,
                                Duration: TimeSpan.FromSeconds(Convert.ToDouble(test.SelectSingleNode(duration).Value, doubleFormat)), 
                                    ErrorStr: errorMsg);
                        }
                        else // Добавление проваленного теста.
                        {
                            iOutput.AddFailed(Name: test.SelectSingleNode(name).InnerText, Path: test.SelectSingleNode(fullname).Value,
                                Duration: TimeSpan.FromSeconds(Convert.ToDouble(test.SelectSingleNode(duration).Value, doubleFormat)), 
                                    ErrorStr: errorMsg);
                        }
                        break;
                }         
            }
        }

    }
}
