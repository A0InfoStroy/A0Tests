using System;
using System.IO;
using System.Text;

namespace A0Tests.Runner
{
    class SaveToTxt : SaveBase
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        private readonly string filePath;
        public SaveToTxt(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException("filePath must be initialized");

        }

        public override void Save(ITestsResult iTestResult)
        {
           
            WriteLine("\n" + totalCount + iTestResult.CountOfAll);
            WriteLine(countOfPassed + iTestResult.CountOfPassed);
            WriteLine(countOfFailed + iTestResult.CountOfFailed);
            WriteLine(countOfTimeOut + iTestResult.CountOfTimeOut);
            WriteLine(totalTime + iTestResult.TotalTime.ToString("hh':'mm':'ss':'fff"));
            WriteLine("");

            //Вывод данных
            foreach (ResultInfo test in iTestResult.GetTimeOut())
            {
                WriteLine(name + test.Name);
                WriteLine(path + test.Path);
                WriteLine(duration + test.Duration.ToString("ss':'fff"));
                WriteLine(result + test.Result);
                WriteLine(error + test.ErrorMsg);
                WriteLine("");
            }
            foreach (ResultInfo test in iTestResult.GetFailed())
            {
                WriteLine(name + test.Name);
                WriteLine(path + test.Path);
                WriteLine(duration + test.Duration.ToString("ss':'fff"));
                WriteLine(result + test.Result);
                WriteLine(error + test.ErrorMsg);
                WriteLine("");
            }
            foreach (ResultInfo test in iTestResult.GetPassed())
            {
                WriteLine(name + test.Name);
                WriteLine(path + test.Path);
                WriteLine(duration + test.Duration.ToString("ss':'fff"));
                WriteLine(result + test.Result);
                WriteLine("");
            }

            SaveFile();
        }

        /// <summary>
        /// Добавление новой строки в stringBuilder.
        /// </summary>
        private void WriteLine(string msg)
        {
            stringBuilder.Append(msg).Append(Environment.NewLine);
        }

        /// <summary>
        /// Сохранение итогового файла.
        /// </summary>
        private void SaveFile()
        {
            
            if (stringBuilder != null && stringBuilder.Length > 0)
            {
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    file.Write(stringBuilder.ToString());
                }
            }
        }

    }
}
