// $Date: 2021-08-16 14:54:53 +0300 (Пн, 16 авг 2021) $
// $Revision: 563 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    // <summary>
    /// Сервис для определения формата вывода.
    /// </summary>
    class SaveOption
    {
        private readonly string resTxtFilePath;
        private readonly string resXmlFilePath;

        public SaveOption(string resTxtFilePath, string resXmlFilePath)
        {
            this.resTxtFilePath = resTxtFilePath;
            this.resXmlFilePath = resXmlFilePath;
        }
        public ISave SaveSelection()
        {

            if (!String.IsNullOrEmpty(resTxtFilePath))
            {
                return new SaveToTxt(resTxtFilePath);
            }
            else if (!String.IsNullOrEmpty(resXmlFilePath))
            {
                return new SaveToXml(resXmlFilePath);
            }
            else
            {
                return new SaveToConsole();
            }

        }
    }
}
