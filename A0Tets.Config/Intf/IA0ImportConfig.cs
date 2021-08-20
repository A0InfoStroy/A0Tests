// $Date: 2021-01-26 14:23:04 +0300 (Вт, 26 янв 2021) $
// $Revision: 508 $
// $Author: agalkin $
// Интерфейсы пользовательских настроек

namespace A0Tests.Config
{
    using System.Runtime.InteropServices.ComTypes;

    /// <summary>
    /// Предоставляет потоки из импортируемых файлов объектных смет.
    /// </summary>
    public interface IA0ImportConfig
    {
        /// <summary>
        /// Получает поток из файла ОС в формате А0.
        /// </summary>
        IStream ImportFrom { get; }

        /// <summary>
        /// Получает поток из файла ОС в формате АРПС 1.
        /// </summary>
        IStream ImportFromARPS { get; }

        /// <summary>
        /// Получает поток из файла ОС в формате XMLGrand
        /// </summary>
        IStream ImportFromXmlGrand { get; }
    }
}