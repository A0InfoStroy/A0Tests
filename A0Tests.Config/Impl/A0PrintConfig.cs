// $Date: 2021-01-26 14:21:57 +0300 (Вт, 26 янв 2021) $
// $Revision: 507 $
// $Author: agalkin $
// Настройки для тестов печати

namespace A0Tests.Config
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Класс для передачи параметров настроек тестов печати в другие сборки.
    /// </summary>
    internal class A0PrintConfig : IA0PrintConfig
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0PrintConfig".
        /// </summary>
        /// <param name="configFields">Настройки тестов печати из xml-файла.</param>
        public A0PrintConfig(PrintConfigFields configFields)
        {
            this.PrintTemplatePath = configFields.PrintTemplatePath;
        }

        /// <summary>
        /// Получает путь к файлам шаблонов печати.
        /// </summary>
        public string PrintTemplatePath { get; }
    }

    /// <summary>
    /// Класс для парсинга раздела настроек печати из xml-файла.
    /// </summary>
    [DataContract(Name = "configuration", Namespace = "")]
    internal class PrintConfigSection : IConfigFields<PrintConfigFields>
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "print" в xml-файле.
        /// </summary> 
        [DataMember(Name = "print", IsRequired = true)]
        public PrintConfigFields ConfigFields { get; set; }
    }

    /// <summary>
    /// Класс для парсинга элементов раздела настроек печати из xml-файла.
    /// </summary>
    [DataContract(Name = "print", Namespace = "")]
    internal class PrintConfigFields
    {
        /// <summary>
        /// Получает или устанавливает значение элемента "printTemplates" в xml-файле.
        /// </summary> 
        [DataMember(Name = "printTemplates", Order = 1, IsRequired = true)]
        public string PrintTemplatePath { get; set; }
    }
}