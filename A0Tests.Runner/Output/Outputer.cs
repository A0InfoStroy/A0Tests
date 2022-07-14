// $Date: 2021-07-16 10:15:28 +0300 (Пт, 16 июл 2021) $
// $Revision: 543 $
// $Author: eloginov $

using System;
using System.IO;
using System.Text;

namespace A0Tests.Runner
{
    /// <summary>
    /// Класс содержит методы для вывода информации на консоль и в файл
    /// </summary>
    public class Outputer
    {
        private StringBuilder logString = new StringBuilder();

        /// <summary>
        /// Вывод на консоль и в файл, если была включена соотвествующая опция.
        /// </summary>
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
            logString.Append(str).Append(Environment.NewLine);
        }


        /// <summary>
        /// Сохранение итогового текстового файла.
        /// </summary>
        public void SaveOut(string resultPath = "res.txt", bool Append = false) //<- default 'false', 'true' Append the log to an existing file.
        {
            if (logString != null && logString.Length > 0)
            {
                if (Append)
                {
                    using (StreamWriter file = File.AppendText(resultPath))
                    {
                        file.Write(logString.ToString());
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(resultPath))
                    {
                        file.Write(logString.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события вывода в консоль/файл.
        /// </summary>
        public void DisplayMessage(string message)
        {
            WriteLine(message);
        }

        /// <summary>
        /// Обработчик для цветной печати в консоль.
        /// </summary>
        public void DisplayMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteLine(message);
            Console.ResetColor();
        }

    }
}
