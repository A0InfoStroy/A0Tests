// $Date: 2021-04-13 12:39:43 +0300 (Вт, 13 апр 2021) $
// $Revision: 530 $
// $Author: agalkin $
// Настройки для тестов импорта

namespace A0Tests.Runner
{
    using System;
    using NUnit.Engine;
    class Program
    {
        static void Main(string[] args)
        {
            //string path = typeof(LateTests.TestClass).Assembly.Location;
            //TestPackage package = new TestPackage(path);
            //package.AddSetting("WorkDirectory", Environment.CurrentDirectory);

            //ITestEngine engine = TestEngineActivator.CreateInstance();
            //ITestFilterService filterService = engine.Services.GetService<ITestFilterService>();
            //ITestFilterBuilder builder = filterService.GetTestFilterBuilder();
            //TestFilter filter = builder.GetFilter();

            //using (ITestRunner runner = engine.GetRunner(package))
            //{
            //    // запуск тестов
            //    var result = runner.Run(null, filter);
            //}
        }
    }
}
