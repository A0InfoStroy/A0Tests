// $Date: 2021-01-26 14:23:04 +0300 (Вт, 26 янв 2021) $
// $Revision: 508 $
// $Author: agalkin $
// Настройки для тестов импорта

namespace A0Tests.Config
{
    using System.IO;
    using System.Runtime.InteropServices.ComTypes;
    using System.Runtime.Serialization;
    using static FileStreamHelper;

    /// <summary>
    /// Класс для передачи потоков импортируемых файлов в другие сборки.
    /// </summary>
    internal class A0ImportConfig : IA0ImportConfig
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0ImportConfig".
        /// </summary>
        /// <param name="importFrom">Поток файла ОС формата А0.</param>
        /// <param name="importFromARPS">Поток файла ОС формата АРПС 1.</param>
        /// <param name="importFromXmlGrand">Поток файла ОС формата А0.</param>
        public A0ImportConfig(IStream importFrom, IStream importFromARPS, IStream importFromXmlGrand)
        {
            this.ImportFrom = importFrom;
            this.ImportFromARPS = importFromARPS;
            this.ImportFromXmlGrand = importFromXmlGrand;
        }

        /// <summary>
        /// Получает поток из файла ОС в формате А0.
        /// </summary>
        public IStream ImportFrom { get; }

        /// <summary>
        /// Получает поток из файла ОС в формате АРПС 1.
        /// </summary>
        public IStream ImportFromARPS { get; }

        /// <summary>
        /// Получает поток из файла ОС в формате XMLGrand
        /// </summary>
        public IStream ImportFromXmlGrand { get; }
    }

    /// <summary>
    /// Класс для парсинга раздела настроек тестов импорта из xml-файла.
    /// </summary>   
    [DataContract(Name = "configuration", Namespace = "")]
    internal class ImportConfigSection : IConfigFields<ImportConfigFields>
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "import" в xml-файле.
        /// </summary> 
        [DataMember(Name = "import", IsRequired = true)]
        public ImportConfigFields ConfigFields { get; set; }
    }

    /// <summary>
    /// Класс для парсинга элементов раздела настроек тестов импорта из xml-файла.
    /// </summary>   
    [DataContract(Name = "import", Namespace = "")]
    internal class ImportConfigFields
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "importFrom" в xml-файле.
        /// </summary> 
        [DataMember(Name = "importFrom", Order = 1, IsRequired = true)]
        public string ImportFrom { get; set; }

        /// <summary>
        /// Получает или устанавливает значение элемента "importFromAPRS" в xml-файле.
        /// </summary> 
        [DataMember(Name = "importFromARPS", Order = 2, IsRequired = true)]
        public string ImportFromARPS { get; set; }

        /// <summary>
        /// Получает или устанавливает значение элемента "importFromXmlGrand" в xml-файле.
        /// </summary> 
        [DataMember(Name = "importFromXmlGrand", Order = 3, IsRequired = true)]
        public string ImportFromXmlGrand { get; set; }
    }

    /// <summary>
    /// Обеспечивает создание потоков для чтения или записи в файлы.
    /// </summary>
    internal class ObjectEstimateConverter
    {
        /// <summary>
        /// Создает поток из файла.
        /// </summary>
        /// <param name="filePath">Путь импортируемого файла.</param>
        /// <returns>Поток для чтения.</returns>
        internal IStream CreateStream(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден", filePath);
            }

            uint result = SHCreateStreamOnFileEx(filePath, STGM_READ | STGM_SHARE_DENY_NONE, 0, false, null, out IStream stream);
            if (result != 0)
            {
                throw new FileLoadException("Ошибка чтения файла", filePath);
            }

            return stream;
        }
    }
}