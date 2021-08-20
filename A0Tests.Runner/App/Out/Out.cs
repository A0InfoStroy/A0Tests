// $Date: 2021-07-01 10:11:39 +0300 (Чт, 01 июл 2021) $
// $Revision: 540 $
// $Author: eloginov $

using System;
using System.Collections.Generic;
using System.Linq;

namespace A0Tests.Runner
{
    class Out : IOut, ITestsResult
    {
        public int CountOfAll { get { return m_Results.Count; } }
        public int CountOfPassed { get { return m_Results.Count(test => test.Result == Kind.Passed); } }
        public int CountOfFailed { get { return m_Results.Count(test => test.Result == Kind.Failed); } }
        public int CountOfTimeOut { get { return m_Results.Count(test => test.Result == Kind.TimeOut); } }

        public TimeSpan TotalTime 
        {
            get
            {
                TimeSpan totalTime = TimeSpan.FromMilliseconds(0);
                foreach (ResultInfo test in m_Results)
                {
                    totalTime += test.Duration;
                }
                return totalTime;
            }
        }


        private List<ResultInfo> m_Results = new List<ResultInfo>(); //список со всеми тестами

        public void AddFailed(string Name, string Path, TimeSpan Duration, string ErrorStr)
        {
            m_Results.Add(new ResultInfo(Name, Path, Duration, Kind.Failed, ErrorStr));
        }

        public void AddPassed(string Name, string Path, TimeSpan Duration)
        {
            m_Results.Add(new ResultInfo(Name, Path, Duration, Kind.Passed));
        }

        public void AddTimeOut(string Name, string Path, TimeSpan Duration, string ErrorStr)
        {
            m_Results.Add(new ResultInfo(Name, Path, Duration, Kind.TimeOut, ErrorStr));
        }

        public List<ResultInfo> GetFailed()
        {
            List<ResultInfo> failed = m_Results.FindAll(test => test.Result == Kind.Failed);
            return failed;
        }

        public List<ResultInfo> GetPassed()
        {
            List<ResultInfo> passed = m_Results.FindAll(test => test.Result == Kind.Passed);
            return passed;
        }

        public List<ResultInfo> GetTimeOut()
        {
            List<ResultInfo> timeOut = m_Results.FindAll(test => test.Result == Kind.TimeOut);
            return timeOut;
        }

    }
}
