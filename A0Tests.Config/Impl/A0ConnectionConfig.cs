// $Date: 2020-11-18 14:44:24 +0300 (Ср, 18 ноя 2020) $
// $Revision: 429 $
// $Author: agalkin $
// Настройки для тестов соединения 

namespace A0Tests.Config
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Класс для передачи параметров настроек тестов соединения с БД А0 в другие сборки.
    /// </summary>
    internal class A0ConnectionConfig : IA0ConnectionConfig
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0ConnectionConfig".
        /// </summary>
        /// <param name="config">Настройки тестов соединения с БД А0 из xml-файла.</param>
        public A0ConnectionConfig(ConnectionConfigFields configFields)
        {
            this.ConnectTimeoutMS = configFields.ConnectTimeoutMS;
        }

        /// <summary>
        /// Получает время ожидания установки соединения в мс.
        /// </summary>
        public int ConnectTimeoutMS { get; }
    }

    /// <summary>
    /// Класс для парсинга раздела настроек тестов соединения с БД А0 из xml-файла.
    /// </summary>   
    [DataContract(Name = "configuration", Namespace = "")]
    internal class ConnectionConfigSection : IConfigFields<ConnectionConfigFields>
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "connection" в xml-файле.
        /// </summary> 
        [DataMember(Name = "connection", IsRequired = true)]
        public ConnectionConfigFields ConfigFields { get; set; }
    }

    /// <summary>
    /// Класс для парсинга элементов раздела настроек тестов соединения с БД А0 из xml-файла.
    /// </summary>   
    [DataContract(Name = "connection", Namespace = "")]
    internal class ConnectionConfigFields
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "testConnectTimeOutMS" в xml-файле.
        /// </summary>
        [DataMember(Name = "testConnectTimeOutMS", Order = 1, IsRequired = true)]
        public int ConnectTimeoutMS { get; set; }
    }
}