// $Date: 2021-01-27 15:18:49 +0300 (Ср, 27 янв 2021) $
// $Revision: 510 $
// $Author: agalkin $
// Парсинг данных из файла настроек

namespace A0Tests.Config
{
    using System.IO;
    using System.Runtime.InteropServices.ComTypes;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// Предоставляет операции для получения параметров пользовательских настроек.
    /// </summary>
    public static class UserConfigParser
    {
        /// <summary>
        /// Получает основные параметры соединения с БД А0.
        /// </summary>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Интерфейс получение основых настроек.</returns>
        public static IA0BaseConfig GetA0BaseConfig(string xmlText) =>
            new A0BaseConfig(GetConfig<BaseConfigSection, BaseConfigFields>(xmlText));

        /// <summary>
        /// Получает параметры тестов соединения с БД А0.
        /// </summary>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Интерфейс получения настроек тестов соединения.</returns>
        public static IA0ConnectionConfig GetA0ConnectionConfig(string xmlText) =>
            new A0ConnectionConfig(GetConfig<ConnectionConfigSection, ConnectionConfigFields>(xmlText));

        /// <summary>
        /// Получает параметры тестов параллельных соединений с БД А0.
        /// </summary>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Интерфейс получения настроек тестов параллельных соединений.</returns>
        public static IA0MultiConnectionConfig GetA0MultiConnectionConfig(string xmlText) =>
            new A0MultiConnectionConfig(GetConfig<MultiConnectionConfigSection, MultiConnectionConfigFields>(xmlText));

        /// <summary>
        /// Получает потоки импортируемых файлов.
        /// </summary>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Интерфейс получения потоков импортируемых файлов.</returns>
        public static IA0ImportConfig GetA0ImportConfig(string xmlText)
        {
            ImportConfigFields fields = GetConfig<ImportConfigSection, ImportConfigFields>(xmlText);
            ObjectEstimateConverter converter = new ObjectEstimateConverter();
            IStream fromA0 = converter.CreateStream(fields.ImportFrom);
            IStream fromARPS = converter.CreateStream(fields.ImportFromARPS);
            IStream fromXmlGrand = converter.CreateStream(fields.ImportFromXmlGrand);
            return new A0ImportConfig(fromA0, fromARPS, fromXmlGrand);
        }

        /// <summary>
        /// Получает параметры тестов печати.
        /// </summary>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Интерфейс получения настроек тестов печати.</returns>
        public static IA0PrintConfig GetA0PrintConfig(string xmlText) =>
            new A0PrintConfig(GetConfig<PrintConfigSection, PrintConfigFields>(xmlText));

        /// <summary>
        /// Получает элементы выбранного раздела настроек из xml-текста.
        /// </summary>
        /// <typeparam name="T">Класс предоставляющий раздел настроек из xml-текста.</typeparam>
        /// <typeparam name="U">Класс предоставляющий элементы раздела настроек из xml-текста.</typeparam>
        /// <param name="xmlText">Текст xml-файла.</param>
        /// <returns>Класс предоставляющий элементы раздела настроек из xml-текста.</returns>
        private static U GetConfig<T, U>(string xmlText) where T : class, IConfigFields<U>
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlText)))
            {
                T section = (T)serializer.ReadObject(ms);
                return section.ConfigFields;
            }
        }
    }
}