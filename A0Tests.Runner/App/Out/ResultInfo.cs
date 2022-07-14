// $Date: 2021-06-29 01:10:47 +0300 (Вт, 29 июн 2021) $
// $Revision: 539 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    /// <summary>
    /// Перечень результатов выполнения теста.
    /// </summary>
    public enum Kind
    {
        Passed,
        Failed,
        TimeOut
    }

    readonly struct ResultInfo
    {
        public string Name { get; }
        public string Path { get; }
        public TimeSpan Duration { get; }
        public Kind Result { get; }
        public string ErrorMsg { get; }

        public ResultInfo(string Name, string Path, TimeSpan Duration, Kind Result, string ErrorMsg = null)
        {
            this.Name = Name;
            this.Path = Path;
            this.Duration = Duration;
            this.Result = Result;
            this.ErrorMsg = ErrorMsg;
        }
    }
}
