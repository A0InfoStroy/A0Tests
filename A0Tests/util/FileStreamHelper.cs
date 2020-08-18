// $Date: 2020-08-17 15:21:59 +0300 (Пн, 17 авг 2020) $
// $Revision: 372 $
// $Author: agalkin $
// Вспомогательный класс для экспорта/импорта файлов

namespace A0Tests
{
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;

    /// <summary>
    /// Вспомогательный класс для экспорта/импорта файлов.
    /// </summary>
    public static class FileStreamHelper
    {
        /// <summary>
        /// Флаг который указывает, что объект доступен только для чтения.
        /// </summary>
        public const uint STGM_READ = 0x00000000;

        /// <summary>
        /// Флаг который указывает, что последующим открытиям объекта не запрещается доступ для чтения или записи.
        /// </summary>
        public const uint STGM_SHARE_DENY_NONE = 0x00000040;

        /// <summary>
        /// Открывает или создает файл и извлекает поток для чтения или записи в этот файл.
        /// </summary>
        /// <param name="fileName">Путь к файлу.</param>
        /// <param name="grfMode">Одно или несколько значений STGM, для указания режима доступа к файлу и способа создания и удаления объекта.</param>
        /// <param name="attributes">Одно или несколько значений флагов, указывающих атрибуты файла в случае создания нового файла.</param>
        /// <param name="create">Признак обрабоки файлов при создании потока.</param>
        /// <param name="reserved">Зарезервированнй IStream.</param>
        /// <param name="stream">Получает указатель интерфейса IStream для потока, связанного с файлом.</param>
        /// <returns>Код возврата.</returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern uint SHCreateStreamOnFileEx(string fileName, uint grfMode, uint attributes, bool create, IStream reserved, out IStream stream);
    }
}