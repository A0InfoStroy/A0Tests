// $Date: 2020-10-19 15:11:53 +0300 (Пн, 19 окт 2020) $
// $Revision: 390 $
// $Author: agalkin $
// Интерфейсы пользовательских настроек

namespace A0Tests.Config
{
    /// <summary>
    /// Предоставляет настройки для тестов печати A0Service.
    /// </summary>
    public interface IA0PrintConfig
    {
        /// <summary>
        /// Получает путь к файлам шаблонов печати.
        /// </summary>
        string PrintTemplatePath { get; }
    }
}