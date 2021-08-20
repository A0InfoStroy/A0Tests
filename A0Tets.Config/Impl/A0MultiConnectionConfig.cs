// $Date: 2020-11-18 15:20:02 +0300 (Ср, 18 ноя 2020) $
// $Revision: 430 $
// $Author: agalkin $
// Настройки для тестов параллельных соединений

namespace A0Tests.Config
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Класс для передачи параметров настроек тестов параллельных соединений с БД А0 в другие сборки.
    /// </summary>
    internal class A0MultiConnectionConfig : IA0MultiConnectionConfig
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0MultiConnectionConfig".
        /// </summary>
        /// <param name="config">Настройки тестов параллельных соединений с БД А0 из xml-файла.</param>
        public A0MultiConnectionConfig(MultiConnectionConfigFields config)
        {
            this.MultiConnectCount = config.MultiConnectCount;
            this.MultiConnectTimeoutMS = config.MultiConnectTimeoutMS;
        }

        /// <summary>
        /// Получает количество одновременно устанавливаемых соединений.
        /// </summary>
        public int MultiConnectCount { get; }

        /// <summary>
        /// Получает время ожидания установки соединения для параллельных соединений в мс.
        /// </summary>
        public int MultiConnectTimeoutMS { get; }
    }

    /// <summary>
    /// Класс для парсинга раздела настроек тестов параллельных соединений с БД А0 из xml-файла.
    /// </summary>   
    [DataContract(Name = "configuration", Namespace = "")]
    internal class MultiConnectionConfigSection : IConfigFields<MultiConnectionConfigFields>
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "multiConnection" в xml-файле.
        /// </summary> 
        [DataMember(Name = "multiConnection", IsRequired = true)]
        public MultiConnectionConfigFields ConfigFields { get; set; }
    }

    /// <summary>
    /// Класс для парсинга элементов раздела настроек тестов параллельных соединений с БД А0 из xml-файла.
    /// </summary>   
    [DataContract(Name = "multiConnection", Namespace = "")]
    internal class MultiConnectionConfigFields
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "testMultiConnectCount" в xml-файле.
        /// </summary> 
        [DataMember(Name = "testMultiConnectCount", Order = 1, IsRequired = true)]
        public int MultiConnectCount { get; set; }

        /// <summary>
        /// Получает или устанавливает значение элемента "testMultiConnectTimeOutMS" в xml-файле.
        /// </summary> 
        [DataMember(Name = "testMultiConnectTimeOutMS", Order = 2, IsRequired = true)]
        public int MultiConnectTimeoutMS { get; set; }
    }
}