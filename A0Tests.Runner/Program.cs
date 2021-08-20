// $Date: 2020-11-20 17:01:23 +0300 (Пт, 20 ноя 2020) $
// $Revision: 433 $
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
            string path = typeof(LateTests.TestClass).Assembly.Location;
            TestPackage package = new TestPackage(path);
            package.AddSetting("WorkDirectory", Environment.CurrentDirectory);

            ITestEngine engine = TestEngineActivator.CreateInstance();
            ITestFilterService filterService = engine.Services.GetService<ITestFilterService>();
            ITestFilterBuilder builder = filterService.GetTestFilterBuilder();
            TestFilter filter = builder.GetFilter();

            using (ITestRunner runner = engine.GetRunner(package))
            {
                // запуск тестов
                var result = runner.Run(null, filter);
            }
        }
    }
}
