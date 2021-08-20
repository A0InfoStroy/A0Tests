// $Date: 2020-11-18 14:44:24 +0300 (Ср, 18 ноя 2020) $
// $Revision: 429 $
// $Author: agalkin $
// Данные для соединения с БД А0

namespace A0Tests.Config
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Класс для передачи параметров основных пользовательских настроек в другие сборки.
    /// </summary>
    internal class A0BaseConfig : IA0BaseConfig
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0BaseConfig".
        /// </summary>
        /// <param name="config">Основные настройки из xml-файла.</param>
        public A0BaseConfig(BaseConfigFields config)
        {
            this.ConnectionString = config.ConnectionString;
            this.UserName = config.UserName;
            this.Password = config.Password;
        }

        /// <summary>
        /// Получает строку подключения к БД А0.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Получает имя пользователя.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Получает пароль.
        /// </summary>
        public string Password { get; }
    }

    /// <summary>
    /// Класс для парсинга раздела основных пользовательских настроек из xml-файла.
    /// </summary>   
    [DataContract(Name = "configuration", Namespace = "")]
    internal class BaseConfigSection : IConfigFields<BaseConfigFields>
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "base" в xml-файле.
        /// </summary> 
        [DataMember(Name = "base", IsRequired = true)] // IsRequired = true позволяет сгенерировать исключение при неудачной попытке спарсить данные.
        public BaseConfigFields ConfigFields { get; set; }
    }

    /// <summary>
    /// Класс для парсинга элементов раздела основных пользовательских настроек из xml-файла.
    /// </summary>
    [DataContract(Name = "base", Namespace = "")]
    internal class BaseConfigFields
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "connectionString" в xml-файле.
        /// </summary>
        [DataMember(Name = "connectionString", Order = 1, IsRequired = true)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Получает или устанавливает значение элемента "userName" в xml-файле.
        /// </summary>
        [DataMember(Name = "userName", Order = 2, IsRequired = true)]
        public string UserName { get; set; }

        /// <summary>
        /// Получает или устанавливает значение элемента "password" в xml-файле.
        /// </summary>
        [DataMember(Name = "password", Order = 3, IsRequired = true)]
        public string Password { get; set; }
    }
}