// $Date: 2021-08-19 15:15:05 +0300 (Чт, 19 авг 2021) $
// $Revision: 567 $
// $Author: eloginov $

namespace A0Tests.Runner
{
    /// <summary>
    /// Базовый класс для вывода результатов в файл и консоль. 
    /// </summary>
    abstract class SaveBase : ISave
    {
        protected const string totalCount = "Total count: ";
        protected const string countOfPassed = "Count of passed: ";
        protected const string countOfFailed = "Count of failed: ";
        protected const string countOfTimeOut = "Count of time out: ";
        protected const string totalTime = "Total time (hh:mm:ss:ms) : ";

        protected const string name = "Name: ";
        protected const string path = "Path: ";
        protected const string duration = "Duration (s:ms): ";
        protected const string result = "Result: ";
        protected const string error = "Error: ";

        public abstract void Save(ITestsResult testsResult);
    }
}
