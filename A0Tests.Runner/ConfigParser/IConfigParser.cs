// $Date: 2021-07-21 12:04:01 +0300 (Ср, 21 июл 2021) $
// $Revision: 545 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    /// <summary>
    /// Разбор аргументов командной строки
    /// </summary>
    interface IConfigParser
    {

        /// <summary>
        /// Разбор аргументов командной строки и формирование структуры с настройками.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        Properties Parse(string[] args); 
    }
}
