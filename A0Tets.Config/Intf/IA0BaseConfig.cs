// $Date: 2020-10-23 15:31:39 +0300 (Пт, 23 окт 2020) $
// $Revision: 394 $
// $Author: agalkin $
// Интерфейсы пользовательских настроек

namespace A0Tests.Config
{
    /// <summary>
    /// Предоставляет основные параметры соединения с БД А0.
    /// </summary>
    public interface IA0BaseConfig
    {
        /// <summary>
        /// Получает строку подключения к БД А0.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Получает имя пользователя.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Получает пароль.
        /// </summary>
        string Password { get; }
    }
}