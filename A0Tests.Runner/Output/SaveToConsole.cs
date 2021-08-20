// $Date: 2021-08-19 15:15:05 +0300 (Чт, 19 авг 2021) $
// $Revision: 567 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    /// <summary>
    /// Вывод отчета в консоль.
    /// </summary>
    class SaveToConsole : SaveBase
    {
        public override void Save(ITestsResult iTestResult)
        {
            
            OutToConsole("\n" + totalCount + iTestResult.CountOfAll);
            OutToConsole(countOfPassed + iTestResult.CountOfPassed);
            OutToConsole(countOfFailed + iTestResult.CountOfFailed);
            OutToConsole(countOfTimeOut + iTestResult.CountOfTimeOut);
            OutToConsole(totalTime + iTestResult.TotalTime.ToString("hh':'mm':'ss':'fff") + "\n");

            //Вывод данных
            foreach (ResultInfo test in iTestResult.GetTimeOut())
            {
                OutToConsole(name + test.Name, ConsoleColor.Yellow);
                OutToConsole(path + test.Path);
                OutToConsole(duration + test.Duration.ToString("ss':'fff"));
                OutToConsole(result + test.Result, ConsoleColor.Magenta);
                OutToConsole(error + test.ErrorMsg + "\n");
            }
            foreach (ResultInfo test in iTestResult.GetFailed())
            {
                OutToConsole(name + test.Name, ConsoleColor.Yellow);
                OutToConsole(path + test.Path);
                OutToConsole(duration + test.Duration.ToString("ss':'fff"));
                OutToConsole(result + test.Result, ConsoleColor.Red);
                OutToConsole(error + test.ErrorMsg + "\n");
            }
            foreach (ResultInfo test in iTestResult.GetPassed())
            {
                OutToConsole(name + test.Name, ConsoleColor.Yellow);
                OutToConsole(path + test.Path);
                OutToConsole(duration + test.Duration.ToString("ss':'fff"));
                OutToConsole(result + test.Result + "\n", ConsoleColor.Green);
            }
        }

        private void OutToConsole(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            OutToConsole(msg);
            Console.ResetColor();
        }

        private void OutToConsole(string msg)
        {
            Console.WriteLine(msg);
        }
    }


}
