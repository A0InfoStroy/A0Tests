// $Date: 2021-07-01 10:11:39 +0300 (Чт, 01 июл 2021) $
// $Revision: 540 $
// $Author: eloginov $

using System;
using System.Collections.Generic;

namespace A0Tests.Runner
{
    /// <summary>
    /// Получение результатов тестирования.
    /// </summary>
    interface ITestsResult
    {
        /// <summary>
        /// Общее количество тестов.
        /// </summary>
        int CountOfAll { get; }
        /// <summary>
        /// Количество пройденных тестов.
        /// </summary>
        int CountOfPassed { get; }
        /// <summary>
        /// Количество проваленных тестов.
        /// </summary>
        int CountOfFailed { get; }
        /// <summary>
        /// Количество тестов с превышенным лимитом времени.
        /// </summary>
        int CountOfTimeOut { get; }
        /// <summary>
        /// Общее время выполнения всех тестов.
        /// </summary>
        TimeSpan TotalTime { get; }


        /// <summary>
        /// Отбор успешно пройденных тестов.
        /// </summary>
        List<ResultInfo> GetPassed();
        /// <summary>
        /// Отбор проваленных тестов с их описанием.
        /// </summary>
        List<ResultInfo> GetFailed();
        /// <summary>
        /// Отбор тестов с превышенным лимитом времени. 
        /// </summary>
        List<ResultInfo> GetTimeOut();

    }
}
