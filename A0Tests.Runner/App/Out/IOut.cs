// $Date: 2021-06-29 01:10:47 +0300 (Вт, 29 июн 2021) $
// $Revision: 539 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    /// <summary>
    /// Сохранение результатов тестирования.
    /// </summary>
    interface IOut
    {
        /// <summary>
        /// Добавление успешно пройденного теста.
        /// </summary>
        void AddPassed(string Name, string Path, TimeSpan Duration);
        /// <summary>
        /// Добавление проваленного теста.
        /// </summary>
        void AddFailed(string Name, string Path, TimeSpan Duration, string ErrorStr);
        /// <summary>
        /// Добавление теста с превышенным лимитом времени.
        /// </summary>
        void AddTimeOut(string Name, string Path, TimeSpan Duration, string ErrorStr); 

    }
}
